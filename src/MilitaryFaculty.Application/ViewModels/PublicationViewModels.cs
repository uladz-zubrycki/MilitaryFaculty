using System;
using System.Collections.Generic;
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
    internal static class PublicationView
    {
        internal class Root : EntityRootViewModel<Publication>
        {
            public Root(Publication model)
                : base(model)
            {
                HeaderViewModel = new Header();
            }

            protected override IEnumerable<ViewModel<Publication>> GetViewModels()
            {
                return new ViewModel<Publication>[]
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
                get { return "Информация о публикации"; }
            }
        }

        internal class Add : AddEntityViewModel<Publication>
        {
            public Add(Publication model): base(model) { }

            public override string Title
            {
                get { return "Добавить публикацию"; }
            }

            public override ICommand AddCommand
            {
                get { return GlobalCommands.Add<Publication>(); }
            }

            protected override IEnumerable<ViewModel<Publication>> GetViewModels()
            {
                return new ViewModel<Publication>[]
                       {
                           new MainInfo(Model),
                           new ExtraInfo(Model)
                       };
            }
        }

        internal class MainInfo : EntityViewModel<Publication>
        {
            public MainInfo(Publication model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<Publication>());
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

            [IntProperty(Label = "Количество страниц:")]
            public int PagesCount
            {
                get { return Model.PagesCount; }
                set { SetModelProperty(m => m.PagesCount, value); }
            }
        }

        internal class ExtraInfo : EntityViewModel<Publication>
        {
            public ExtraInfo(Publication model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<Publication>());
            }

            public override string Title
            {
                get { return "Дополнительная информация"; }
            }

            [EnumProperty(Label = "Тип:")]
            public PublicationType PublicationType
            {
                get { return Model.PublicationType; }
                set { SetModelProperty(m => m.PublicationType, value); }
            }
        }
        
        internal class ListItem : ListItemViewModel<Publication>
        {
            public ListItem(Publication model)
                : base(model)
            {
                TooltipViewModel = new ExtraInfo(Model);
                InitCommands();
            }

            public override string PrimaryInfo
            {
                get { return Model.PagesCount + " стр."; }
            }

            public override string SecondaryInfo
            {
                get { return Model.Name; }
            }

            private void InitCommands()
            {
                Commands.AddRange(new[]
                                  {
                                      CreateBrowsePublicationCommand(),
                                      CreateRemovePublicationCommand()
                                  });
            }

            private ImagedCommandViewModel CreateRemovePublicationCommand()
            {
                const string tooltip = "Удалить публикацию";
                const string imageSource = @"..\Content\remove.png";

                return new ImagedCommandViewModel(GlobalCommands.Remove<Publication>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
            }

            private ImagedCommandViewModel CreateBrowsePublicationCommand()
            {
                const string tooltip = "Подробно";
                const string imageSource = @"..\..\Content\details.png";

                return new ImagedCommandViewModel(GlobalCommands.BrowseDetails<Publication>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
            }

            public static ListItem FromModel(Publication model)
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