using System;
using System.Linq;
using System.Windows;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Application.Views.Reporting;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Excel;

namespace MilitaryFaculty.Application.Handlers
{
    public class ReportingHandlers : ICommandModule
    {
        private readonly IRepository<Cathedra> _cathedraRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IExcelReportingService _excelService;
        private readonly IReportGenerator _reportGenerator;

        public ReportingHandlers(IRepository<Cathedra> cathedraRepository,
                                 IRepository<Person> personRepository,
                                 IExcelReportingService excelService,
                                 IReportGenerator reportGenerator)
        {
            if (cathedraRepository == null)
            {
                throw new ArgumentNullException("cathedraRepository");
            }

            if (personRepository == null)
            {
                throw new ArgumentNullException("personRepository");
            }

            if (excelService == null)
            {
                throw new ArgumentNullException("excelService");
            }

            if (reportGenerator == null)
            {
                throw new ArgumentNullException("reportGenerator");
            }

            _cathedraRepository = cathedraRepository;
            _personRepository = personRepository;
            _excelService = excelService;
            _reportGenerator = reportGenerator;
        }

        public void LoadModule(RoutedCommands commands)
        {
            commands.AddCommand(GlobalCommands.GenerateReport, GenerateReport);
        }

        private void GenerateReport()
        {
            var viewModel = new ReportView.CreateReport(_cathedraRepository,
                                                        _personRepository,
                                                        _excelService,
                                                        _reportGenerator);

            var view = new CreateReport {DataContext = viewModel};
            viewModel.CancelCommand = new SimpleCommand(view.Close);
            viewModel.GenerateReportCommand = new SimpleCommand(() => SaveReport(viewModel, view));

            view.ShowDialog();
        }

        private void SaveReport(ReportView.CreateReport viewModel, CreateReport view)
        {
            // TODO add validation
            try
            {
                var timeInterval = new TimeInterval(viewModel.StartDate, viewModel.EndDate);
                var reportParameters = viewModel.ReportParameters;
                var filePath = viewModel.FilePath;

                if (reportParameters is ReportView.PersonReportParameters)
                {
                    var personParameters = reportParameters as ReportView.PersonReportParameters;
                    var persons = personParameters.ChoosenPersons;
                    var reports =
                        persons.Select(p => _reportGenerator.GenerateProfessorReport(p.Model, timeInterval))
                               .ToList();

                    var report = Report.Unify(reports);
                    _excelService.ExportReport(filePath, report);
                }
                else if (reportParameters is ReportView.CathedraReportParameters)
                {
                    var cathedraParameters = reportParameters as ReportView.CathedraReportParameters;
                    var cathedra = cathedraParameters.SelectedCathedra;
                    var report = _reportGenerator.GenerateCathedraReport(cathedra, timeInterval);
                    _excelService.ExportReport(filePath, report);
                }
                else
                {
                    var report = _reportGenerator.GenerateFacultyReport(timeInterval);
                    _excelService.ExportReport(filePath, report);
                }

                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при генерации отчёта. Проверьте введённые данные",
                                "Ошибка");
            }
        }
    }
}