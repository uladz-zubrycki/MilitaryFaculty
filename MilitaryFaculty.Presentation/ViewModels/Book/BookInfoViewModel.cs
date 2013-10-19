using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class BookInfoViewModel : ViewModel<Book>
    {
        #region Class Properties

        public string Name
        {
            get { return Model.Name; }
            set
            {
                if (value == Name)
                {
                    return;
                }

                Model.Name = value;
                OnPropertyChanged();
            }
        }

        public string PagesCountString
        {
            get { return String.Format("{0} стр.", PagesCount); }
        }

        public int PagesCount
        {
            get { return Model.PagesCount; }

            set
            {
                if (value == PagesCount)
                {
                    return;
                }

                Model.PagesCount = value;
                OnPropertyChanged();
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public BookInfoViewModel(Book model)
            : this(model, EditViewMode.Display)
        {
            // Empty
        }

        public BookInfoViewModel(Book model, EditViewMode mode)
            : base(model)
        {
            const string title = "Основная информация";
            
            Title = title;

            var editCommands = new EditUICommandsPackage<Book>(GlobalAppCommands.UpdateBook, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}