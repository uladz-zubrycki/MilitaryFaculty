using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    internal static class DissertationView
    {
        internal class Root : EntityRootViewModel<Dissertation>
        {
            public Root(Dissertation model)
                : base(model)
            {
                HeaderViewModel = new Header();
            }

            protected override IEnumerable<ViewModel<Dissertation>> GetViewModels()
            {
                return new ViewModel<Dissertation>[]
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
                get { return "Информация о диссертации"; }
            }
        }

        internal class Add : AddEntityViewModel<Dissertation>
        {
            public Add(Dissertation model) : base(model) { }

            public override string Title
            {
                get { return "Добавить диссертацию"; }
            }

            public override ICommand AddCommand
            {
                get { return GlobalCommands.Add<Dissertation>(); }
            }

            protected override IEnumerable<ViewModel<Dissertation>> GetViewModels()
            {
                return new ViewModel<Dissertation>[]
                   {
                       new MainInfo(Model),
                       new ExtraInfo(Model)
                   };
            }
        }

        internal class MainInfo : EntityViewModel<Dissertation>
        {
            public MainInfo(Dissertation model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<Dissertation>());
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
        }

        internal class ExtraInfo : EntityViewModel<Dissertation>
        {
            public ExtraInfo(Dissertation model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<Dissertation>());
            }

            public override string Title
            {
                get { return "Дополнительная информация"; }
            }
          
        }

        internal class ListItem : ListItemViewModel<Dissertation>
        {
            public ListItem(Dissertation model)
                : base(model)
            {
                TooltipViewModel = new ExtraInfo(Model);
                InitCommands();
            }

            public override string PrimaryInfo
            {
                get
                {
                    throw new NotImplementedException();
                }
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

                return new ImagedCommandViewModel(GlobalCommands.Remove<Dissertation>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
            }

            private ImagedCommandViewModel CreateBrowseBookCommand()
            {
                const string tooltip = "Подробно";
                const string imageSource = @"..\..\Content\details.png";

                return new ImagedCommandViewModel(GlobalCommands.BrowseDetails<Book>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
            }

            public static ListItem FromModel(Dissertation model)
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
