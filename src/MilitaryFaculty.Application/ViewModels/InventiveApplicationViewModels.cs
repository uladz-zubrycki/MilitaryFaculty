﻿using System;
using System.Collections.Generic;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class InventiveApplicationView
    {
        internal class Root : EntityRootViewModel<InventiveApplication>
        {
            public Root(InventiveApplication model)
                : base(model)
            {
                HeaderViewModel = new Header();
            }

            protected override IEnumerable<ViewModel<InventiveApplication>> GetViewModels()
            {
                return new ViewModel<InventiveApplication>[]
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
                get { return "Информация об изобретательской заявке"; }
            }
        }

        internal class Add : AddEntityViewModel<InventiveApplication>
        {
            public Add(InventiveApplication model) : base(model) { }

            public override string Title
            {
                get { return "Добавить изобретательскую заявку"; }
            }

            protected override IEnumerable<ViewModel<InventiveApplication>> GetViewModels()
            {
                return new ViewModel<InventiveApplication>[]
                   {
                       new MainInfo(Model),
                       new ExtraInfo(Model)
                   };
            }
        }

        internal class MainInfo : EntityViewModel<InventiveApplication>
        {
            public MainInfo(InventiveApplication model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<InventiveApplication>());
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

        internal class ExtraInfo : EntityViewModel<InventiveApplication>
        {
            public ExtraInfo(InventiveApplication model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<InventiveApplication>());
            }

            public override string Title
            {
                get { return "Дополнительная информация"; }
            }

            [EnumProperty(Label = "Тип заявки:")]
            public InventiveApplicationType Test
            {
                get { return Model.Type; }
                set { SetModelProperty(m => m.Type, value); }
            }

            [EnumProperty(Label = "Статус заявки:")]
            public InventiveApplicationStatus Status
            {
                get { return Model.Status; }
                set { SetModelProperty(m => m.Status, value); }
            }
        }

        internal class ListItem : ListItemViewModel<InventiveApplication>
        {
            public ListItem(InventiveApplication model)
                : base(model)
            {
                TooltipViewModel = new ExtraInfo(Model);
                InitCommands();
            }

            public override string PrimaryInfo
            {
                get { return Model.CreatedAt.ToShortDateString(); }
            }

            public override string SecondaryInfo
            {
                get { return Model.Name; }
            }

            private void InitCommands()
            {
                Commands.AddRange(new[]
                              {
                                  CreateBrowseDetailsCommand(),
                                  CreateRemoveCommand()
                              });
            }

            private ImagedCommandViewModel CreateRemoveCommand()
            {
                const string tooltip = "Удалить заявку";
                const string imageSource = @"..\Content\remove.png";

                return new ImagedCommandViewModel(GlobalCommands.Remove<InventiveApplication>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
            }

            private ImagedCommandViewModel CreateBrowseDetailsCommand()
            {
                const string tooltip = "Подробно";
                const string imageSource = @"..\..\Content\details.png";

                return new ImagedCommandViewModel(GlobalCommands.BrowseDetails<InventiveApplication>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
            }

            public static ListItem FromModel(InventiveApplication model)
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
