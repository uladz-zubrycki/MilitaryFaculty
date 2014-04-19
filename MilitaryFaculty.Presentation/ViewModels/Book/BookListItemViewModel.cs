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
        public BookListItemViewModel(Book model)
            : base(model)
        {
            TooltipViewModel = new BookExtraInfoViewModel(Model);
            InitCommands();
        }

        public override string PrimaryInfo
        {
            get { return Model.PagesCount.ToString(CultureInfo.InvariantCulture) + "стр."; }
        }

        public override string SecondaryInfo
        {
            get { return Model.Name; }
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

            return new ImagedCommandViewModel(Do.BookRemove,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        private ImagedCommandViewModel CreateBrowseBookCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(Browse.BookDetails,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        public static BookListItemViewModel FromModel(Book model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            return new BookListItemViewModel(model);
        }
    }
}