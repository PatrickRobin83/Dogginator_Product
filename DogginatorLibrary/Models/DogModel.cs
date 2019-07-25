/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   DogModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System.Collections.Generic;

namespace de.rietrob.dogginator_product.DogginatorLibrary.Models
{
    public class DogModel
    {
       
        #region Fields

        #endregion

        #region Properties
        /// <summary>
        /// Id from the DogModel out of the Database
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the dog
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Breed of the Dog
        /// </summary>
        public string Breed { get; set; }
        /// <summary>
        /// Dog Fur Color 
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Dog Gender
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Dogs Birthday
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// Date since when the dog is castrated as string
        /// </summary>
        public string CastratedSince { get; set; }
        /// <summary>
        /// Bool says of the dog is permanent castrated
        /// </summary>
        public bool PermanentCastrated { get; set; }
        /// <summary>
        /// If the dog is not permnent castrated how long is the method effective like a hormone stick
        /// </summary>
        public string EffectiveUntil { get; set; }
        /// <summary>
        /// List of diseases of the dog like "needs special food" or "has hips problems" 
        /// </summary>
        public List<DiseasesModel> Diseases {get ; set; }
        /// <summary>
        /// List of Characteristics of the dog like or "cant stay alone for some time" or "jumps into every puddle the dog finds" 
        /// </summary>
        public List<CharacteristicsModel> Characteristics { get; set; }
        /// <summary>
        /// Date of creation in the database automatically filled in
        /// </summary>
        public string Create_Date { get; set; }
        /// <summary>
        /// Date when was the last update of the dog automatically filled from the database
        /// </summary>
        public string Edit_Date { get; set; }
        /// <summary>
        /// List of owner of the dog
        /// </summary>
        public List<CustomerModel> CustomerList { get; set; }
        /// <summary>
        /// is the dog active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// is dog active as a string
        /// </summary>
        public string DogActive { get; set; }
        /// <summary>
        /// Gives back the full dog Name | Breed | Color | Gender | Birthday | DogActive
        /// </summary>
        public string FullDog
        {
            get
            {
                return $"{ Name } { Breed } { Color } { Gender } { Birthday } {DogActive}";
            }
        }

        public string DogAndCustomer
        {
            
            get
            {
                string CustomerName = "";
                if (CustomerList.Count > 0)
                {
                    CustomerName = $"{CustomerList[0].LastName}, {CustomerList[0].FirstName}";
                }
                return $"Hund: {Name} --- Kunde: {CustomerName}";
            }
        }
        #endregion

        #region Contstructor
        /// <summary>
        /// Creates a DogModel with name already filled in
        /// </summary>
        /// <param name="name">string name of the dog</param>
        public DogModel(string name)
        {
            this.Name = name;
        }
        /// <summary>
        /// Default Construtor
        /// </summary>
        public DogModel()
        {
        }
        #endregion

        #region Methods
        #endregion
    }
}
