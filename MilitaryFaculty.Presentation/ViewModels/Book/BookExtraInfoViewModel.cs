using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Common.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class BookExtraInfoViewModel : ViewModel<Book>
    {
        #region Class Properties

        public string BookTypeString
        {
            get { return Model.BookType.GetName(); }
        }

        public IEnumerable<Tuple<BookType, string>> BookTypeList
        {
            get
            {
                return Enum.GetValues(typeof (BookType))
                           .Cast<BookType>()
                           .Select(val => new Tuple<BookType, string>(val, val.GetName()));
            }
        }

        public BookType BookType
        {
            get { return Model.BookType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }
                
                if (value == BookType)
                {
                    return;
                }

                Model.BookType = value;
                OnPropertyChanged();
                OnPropertyChanged("BookTypeString");
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public BookExtraInfoViewModel(Book model)
            : this(model, EditViewMode.Display)
        {
            // Empty
        }

        public BookExtraInfoViewModel(Book model, EditViewMode mode)
            : base(model)
        {
            const string title = "Дополнительная информация";

            Title = title;

            var editCommands = new EditUICommandsPackage<Book>(GlobalAppCommands.UpdateBook, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}