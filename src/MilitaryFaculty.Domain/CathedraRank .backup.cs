//using System;
//using System.Collections.Generic;
//using MilitaryFaculty.Domain.Base;

//namespace MilitaryFaculty.Domain
//{
//    public class ScienceRankMetric : UniqueEntity
//    {
//        public ScienceRankMetric(string name, int maxValue)
//        {
//            if (String.IsNullOrEmpty(name))
//            {
//                throw new ArgumentNullException("name");
//            }

//            Name = name;
//            MaxValue = maxValue;
//        }

//        public string Name { get; private set; }
//        public int MaxValue { get; private set; }
//        public int Value { get; set; }
//    }

//    //TODO too much hardcode 
//    // ReSharper disable DoNotCallOverridableMethodsInConstructor
//    public class ScienceRank : UniqueEntity
//    {
//        public virtual DateTime CreatedAt { get; set; }

//        public virtual ICollection<ScienceRankMetric> Metrics { get; set; }

//        public ScienceRank()
//        {
//            CreatedAt = DateTime.Now;
//            Metrics = new List<ScienceRankMetric>
//                    {
//                        new ScienceRankMetric {}
//                    }
//        }

//        /// <summary>
//        /// Полнота разработки планирующих документов
//        /// </summary>
//        public int PlanningDocuments { get; set; }

//        /// <summary>
//        /// Уровень организации международных конференций
//        /// </summary>
//        public int InternationalConferences { get; set; }

//        /// <summary>
//        /// Уровень организации республиканских конференций
//        /// </summary>
//        public int RepublicanConferences { get;set; }

//        /// <summary>
//        /// Уровень организации вузовских конференций
//        /// </summary>
//        public int UniversityConferences { get; set; }

//        /// <summary>
//        /// Уровень организации республиканских семинаров
//        /// </summary>
//        public int RepublicanSeminars { get; set; }

//        /// <summary>
//        /// Уровень организации вузовских семинаров
//        /// </summary>
//        public int UniversitySeminars { get; set; }

//        /// <summary>
//        /// Уровень организации подготовки научно педагогических работников
//        /// высшей квалификации
//        /// </summary>
//        public int ProfessorTraining { get; set; }

//        /// <summary>
//        /// Уровень организации военно-исторической работы
//        /// </summary>
//        public int HistoricalWork { get; set; }

//        /// <summary>
//        /// Уровень организации работы научных кружков
//        /// </summary>
//        public int ScientificSociety { get; set; }

//        /// <summary>
//        /// Другие частные показатели, характеризующие качество организации научной работы
//        /// </summary>
//        public int OtherScientificOrganisationRates { get; set; }

//        /// <summary>
//        /// Другие частные показатели, характеризующие проведение научных исследований
//        /// </summary>
//        public int OtherScientificResearchRates { get; set; }

//        /// <summary>
//        /// Другие частные показатели, характеризующие подготовку и аттестацию
//        /// работников высшей квалификации
//        /// </summary>
//        public int OtherProfessorTrainingRates { get; set; }
//    }
//    // ReSharper restore DoNotCallOverridableMethodsInConstructor
//}

 //i
