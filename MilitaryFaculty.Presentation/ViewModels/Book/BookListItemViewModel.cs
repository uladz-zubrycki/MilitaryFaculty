using System;
using System.Globalization;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class BookListItemViewModel : ListItemViewModel<Book>
    {
        public override string PrimaryInfo
        {
            get { return Model.PagesCount.ToString(CultureInfo.InvariantCulture) + "стр."; }
        }

        public override string SecondaryInfo
        {
            get { return Model.Name; }
        }

        public BookListItemViewModel(Book model)
            : base(model)
        {
            InitCommands();

            TooltipViewModel = new BookExtraInfoViewModel(Model);
        }

        public static Func<Book, BookListItemViewModel> FromModel()
        {
            return book => new BookListItemViewModel(book);
        }

        private void InitCommands()
        {
            Commands.AddRange(new[]
                              {
                                  CreateBrowseBookCommand(),
                                  CreateRemoveBookCommand()
                              });
        }

        private ImagedCommandViewModel CreateRemoveBookCommand()
        {
            const string tooltip = "Удалить книгу";
            const string imageSource = @"..\Content\remove.png";

            return new ImagedCommandViewModel(Do.Book.Remove,
                Model, tooltip, imageSource);
        }

        private ImagedCommandViewModel CreateBrowseBookCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(Browse.Book.Details,
                Model, tooltip, imageSource);
        }
    }
}