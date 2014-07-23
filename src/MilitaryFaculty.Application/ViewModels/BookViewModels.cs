using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class BookView
    {
        internal class Root : EntityRootViewModel<Book>
        {
            public Root(Book model)
                : base(model)
            {
                HeaderViewModel = new Header();
            }

            protected override IEnumerable<ViewModel<Book>> GetViewModels()
            {
                return new ViewModel<Book>[]
                   {
                       new MainInfo(Model),
                       new ExtraInfo(Model)
                   };
            }
        }

        internal class Header : ViewModel
        {
            public override string Title
            {
                get { return "Информация о книге"; }
            }
        }

        internal class Add : AddEntityViewModel<Book>
        {
            public Add(Book model): base(model) { }

            public override string Title
            {
                get { return "Добавить учебник"; }
            }

            public override ICommand AddCommand
            {
                get { return Do.BookAdd; }
            }

            protected override IEnumerable<ViewModel<Book>> GetViewModels()
            {
                return new ViewModel<Book>[]
                   {
                       new MainInfo(Model),
                       new ExtraInfo(Model)
                   };
            }
        }

        internal class MainInfo : EntityViewModel<Book>
        {
            public MainInfo(Book model)
                : base(model)
            {
                this.Editable(Do.BookSave);
            }

            public override string Title
            {
                get { return "Основная информация"; }
            }

            [TextProperty(Label = "Название:")]
            public string Name
            {
                get { return Model.Name; }
                set { SetModelProperty(m => m.Name, value); }
            }

            [DateProperty(Label = "Дата публикации:")]
            public DateTime CreatedAt
            {
                get { return Model.CreatedAt; }
                set { SetModelProperty(m => m.CreatedAt, value); }
            }

            [IntProperty(Label = "Количество страниц:")]
            public int PagesCount
            {
                get { return Model.PagesCount; }
                set { SetModelProperty(m => m.PagesCount, value); }
            }
        }

        internal class ExtraInfo : EntityViewModel<Book>
        {
            public ExtraInfo(Book model)
                : base(model)
            {
                this.Editable(Do.BookSave);
            }

            public override string Title
            {
                get { return "Дополнительная информация"; }
            }

            [EnumProperty(Label = "Тип книги:")]
            public BookType BookType
            {
                get { return Model.BookType; }
                set { SetModelProperty(m => m.BookType, value); }
            }
        }

        internal class ListItem : ListItemViewModel<Book>
        {
            public ListItem(Book model)
                : base(model)
            {
                TooltipViewModel = new ExtraInfo(Model);
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

            public static ListItem FromModel(Book model)
            {
                if (model == null)
                {
                    throw new ArgumentNullException("model");
                }

                return new ListItem(model);
            }
        }
    }
}