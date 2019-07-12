using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.DogginatorLibrary.DataAccess
{
    public interface IDataConnection
    {
        #region Customer
        /// <summary>
        /// This Method should connect to the Database and get Information about all saved Customers
        /// </summary>
        /// <returns>
        /// Returns a List of CustomerModels
        /// </returns>
        List<CustomerModel> Get_CustomerAll();

        List<CustomerModel> Get_CustomerInactiveAndActive();

        /// <summary>
        /// This Method should connect to the Database and get Information about all saved Dogs
        /// </summary>
        /// <returns>
        /// Returns a List of DogModels
        /// </returns>

        /// <summary>
        /// Adds the Customer Model
        /// </summary>
        /// <returns>
        /// Customer complete with id 
        /// </returns>
        CustomerModel AddCustomer(CustomerModel cModel);

        /// <summary>
        /// Deletes a Customer Model
        /// </summary>
        /// <param name="model"></param>
        void DeleteCustomer(CustomerModel model);

        /// <summary>
        /// Updates a Customer
        /// </summary>
        void UpdateCustomer(CustomerModel cModel);

        NoteModel AddNoteToCustomer(CustomerModel cModel, string note);

        CustomerModel Get_Customer(CustomerModel model);

        void DeleteNoteFromList(NoteModel noteModel, CustomerModel cModel);

        List<CustomerModel> SearchResultsCustomer(string searchText, bool isActiveAndInactive);

        #endregion

        #region Dog

        List<DogModel> Get_DogsAll();
        List<DogModel> Get_DogsInactiveAndActive();
        /// <summary>
        /// Adds the Dog Model
        /// </summary>
        /// <returns>
        /// Dog complete with id 
        /// </returns>
        DogModel AddDog(DogModel dModel, CustomerModel cModel);
        /// <summary>
        /// Updates a Dog
        /// </summary>
        void UpdateDog(DogModel dModel);
        /// <summary>
        /// Saves the new Note about the Customer in the DataBase
        /// </summary>
        /// <param name="cModel">Customermodel with the correct ID</param>
        /// <param name="note">Note to save</param>
        /// <returns>The Notemodel with the ID</returns>
        DogModel AddDogToDatabase(DogModel dModel);
        void AddDogToCustomer(DogModel dModel, CustomerModel cModel);
        List<CustomerModel> GetAllCustomerForDog(DogModel dModel);
        void DeleteDogToCustomerRelation(CustomerModel cModel, DogModel dModel);
        void DeleteDogDiseasesRelation(DogModel dModel);
        void DeleteDogToCharacteristicsRelation(DogModel dModel);
        void DeleteDiseases(DiseasesModel model);
        void DeleteCharacteristics(CharacteristicsModel model);
        void DeleteDogFromDatabase(DogModel model);
        List<DogModel> SearchResultDogs(string searchText, bool activeAndInactive);
        
        #endregion

        #region User
        UserModel IsUserAndPasswordRight(UserModel input);
        void InsertUserToDatabase(UserModel model);
        List<UserModel> GetAllActiveUser();
        void DeleteUserFromDataBase(UserModel model);
        List<UserModel> GetAllUser();
        void UpdateUser(UserModel model);
        List<UserModel> SearchResultUser(string searchText, bool showInactive);
        #endregion

        #region Product
        List<ProductModel> GetAllProducts();
        ProductModel AddProductToDatabase(ProductModel productModel);
        void DeleteProductFromDatabase(ProductModel productModel);
        void UpdateProduct(ProductModel productModel);
        List<ProductModel> SearchResulProducts(string searchText, bool showInactive);

        #endregion

        #region Appointment

        List<AppointmentModel> getAppointmentsForDog(DogModel dogModel);
        AppointmentModel AddAppointmentToDatabase(AppointmentModel apointmentModel);
        bool isAppointmentInDatabase(AppointmentModel appointmentModel);

        bool isDogInTimeSpanAlreadyInDatabase(AppointmentModel appointmentModel);

        #endregion

        #region CityToZipcode

        List<string> getCityToZipcode(string zipCode);

        #endregion
    }
}
