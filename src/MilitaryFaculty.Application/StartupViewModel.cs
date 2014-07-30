using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application
{
    public class StartupViewModel : ViewModel
    {
        public override string Title
        {
            get { return "Приветствуем Вас. \n" +
                         "Данное приложение предназначено для автоматизации расчёта научного рейтинга " +
                         "относительно приказа министра обороны РБ 965 от 10.11.2009. ";
            }
        }
    }
}