using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Annotations;
using MilitaryFaculty.Presentation.Commands;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Excel;
using MilitaryFaculty.Resources;

namespace MilitaryFaculty.Application.ViewModels
{
    [LocalizedEnum(typeof (EnumStrings))]
    public enum ReportType
    {
        Faculty,
        Cathedra,
        Person
    }

    public static class ReportView
    {
        public class CreateReport : ViewModel
        {
            [UsedImplicitly] private string _filePath;
            [UsedImplicitly] private ReportType _reportType;
            [UsedImplicitly] private DateTime _startDate;
            [UsedImplicitly] private DateTime _endDate;
            [UsedImplicitly] private ViewModel _reportParameters;

            private readonly IRepository<Cathedra> _cathedraRepository;
            private readonly IRepository<Person> _personRepository;

            public CreateReport(IRepository<Cathedra> cathedraRepository,
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
                    throw new ArgumentNullException("professorRepository");
                }

                _cathedraRepository = cathedraRepository;
                _personRepository = personRepository;

                StartDate = DateTime.Now;
                EndDate = DateTime.Now;
                SelectPathCommand = new SimpleCommand(ChangePath);
                PropertyChanged += OnPropertyChanged;
            }

            public ICommand CancelCommand { get; set; }
            public ICommand GenerateReportCommand { get; set; }
            public ICommand SelectPathCommand { get; private set; }

            public string FilePath
            {
                get { return _filePath; }
                set { SetValue("FilePath", () => _filePath, value); }
            }

            public ReportType ReportType
            {
                get { return _reportType; }
                set { SetValue("ReportType", () => _reportType, value); }
            }

            public DateTime StartDate
            {
                get { return _startDate; }
                set { SetValue("StartDate", () => _startDate, value); }
            }

            public DateTime EndDate
            {
                get { return _endDate; }
                set { SetValue("EndDate", () => _endDate, value); }
            }

            public ViewModel ReportParameters
            {
                get { return _reportParameters; }
                set { SetValue("ReportParameters", () => _reportParameters, value); }
            }

            private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
            {
                if (args.PropertyName != "ReportType")
                {
                    return;
                }

                switch (ReportType)
                {
                    case ReportType.Faculty:
                        ReportParameters = null;
                        break;

                    case ReportType.Cathedra:
                        ReportParameters = new CathedraReportParameters(_cathedraRepository);
                        break;

                    case ReportType.Person:
                        ReportParameters = new PersonReportParameters(_personRepository);
                        break;
                }
            }

            private void ChangePath()
            {
                var dialog = new SaveFileDialog
                             {
                                 FileName = "Report",
                                 DefaultExt = ".xlsx",
                                 Filter = "Excel documents (.xlsx)|*.xlsx",
                             };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = dialog.FileName;
                }
            }
        }

        public class CathedraReportParameters : ViewModel
        {
            [UsedImplicitly] private Cathedra _selectedCathedra;

            public CathedraReportParameters(IRepository<Cathedra> cathedraRepository)
            {
                if (cathedraRepository == null)
                {
                    throw new ArgumentNullException("cathedraRepository");
                }

                AllCathedras = cathedraRepository.Table.ToList();
                _selectedCathedra = AllCathedras.FirstOrDefault();
            }

            public ICollection<Cathedra> AllCathedras { get; private set; }

            public Cathedra SelectedCathedra
            {
                get { return _selectedCathedra; }
                set { SetValue("SelectedCathedra", () => _selectedCathedra, value); }
            }
        }

        public class PersonItem : ViewModel<Person>
        {
            private readonly PersonReportParameters _owner;

            public PersonItem(Person model, PersonReportParameters owner)
                : base(model)
            {
                _owner = owner;

                RemovePersonCommand = new SimpleCommand(RemovePerson);
            }

            public ICommand RemovePersonCommand { get; set; }

            public override string Title
            {
                get { return Model.FullName.ToString(); }
            }

            private void RemovePerson()
            {
                _owner.ChoosenPersons.Remove(this);
                _owner.AllPersons.Add(this);

                if (_owner.AllPersons.Count == 1)
                {
                    _owner.SelectedPerson = this;
                }
            }
        }

        public class PersonReportParameters : ViewModel
        {
            [UsedImplicitly] private PersonItem _selectedPerson;

            public PersonReportParameters(IRepository<Person> personRepository)
            {
                if (personRepository == null)
                {
                    throw new ArgumentNullException("personRepository");
                }

                AllPersons = CreatePersonCollection(personRepository.Table);
                ChoosenPersons = new ObservableCollection<PersonItem>();
                AddPersonCommand = new SimpleCommand(AddPerson, CanAddPerson);

                _selectedPerson = AllPersons.FirstOrDefault();
            }

            public ICommand AddPersonCommand { get; private set; }
            public ICommand RemovePersonCommand { get; private set; }
            public ObservableCollection<PersonItem> AllPersons { get; private set; }
            public ObservableCollection<PersonItem> ChoosenPersons { get; private set; }

            public PersonItem SelectedPerson
            {
                get { return _selectedPerson; }
                set { SetValue("SelectedPerson", () => _selectedPerson, value); }
            }

            private ObservableCollection<PersonItem> CreatePersonCollection(IEnumerable<Person> persons)
            {
                var items = persons.Select(p => new PersonItem(p, this))
                                   .ToList();

                return new ObservableCollection<PersonItem>(items);
            }

            private void AddPerson()
            {
                ChoosenPersons.Add(SelectedPerson);
                AllPersons.Remove(SelectedPerson);
                SelectedPerson = AllPersons.FirstOrDefault();
            }

            private bool CanAddPerson()
            {
                return !AllPersons.IsEmpty();
            }
        }
    }
}
