/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   IDataConnection.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System.Collections.Generic;


namespace de.rietrob.dogginator_product.DogginatorLibrary.DataAccess
{
    public interface IDataConnection
    {
        #region Customer
        List<CustomerModel> Get_CustomerAll();
        List<CustomerModel> Get_CustomerInactiveAndActive();
        CustomerModel AddCustomer(CustomerModel cModel);
        void DeleteCustomer(CustomerModel model);
        void UpdateCustomer(CustomerModel cModel);
        NoteModel AddNoteToCustomer(CustomerModel cModel, string note);
        CustomerModel Get_Customer(CustomerModel model);
        void DeleteNoteFromList(NoteModel noteModel, CustomerModel cModel);
        List<CustomerModel> SearchResultsCustomer(string searchText, bool isActiveAndInactive);
        #endregion

        #region Dog
        List<DogModel> Get_DogsAll();
        List<DogModel> Get_DogsInactiveAndActive();
        DogModel AddDog(DogModel dModel, CustomerModel cModel);
        void UpdateDog(DogModel dModel);
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
        DogModel GetDog(int id);
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
        List<AppointmentModel> getAppointments();
        AppointmentModel AddAppointmentToDatabase(AppointmentModel apointmentModel);
        bool isAppointmentInDatabase(AppointmentModel appointmentModel);
        bool isDogInTimeSpanAlreadyInDatabase(AppointmentModel appointmentModel);
        void editAppointmentModel(AppointmentModel appointmentModel);
        void deleteAppointmentModel(AppointmentModel appointmentModel);

        #endregion

        #region CityToZipcode
        List<string> getCityToZipcode(string zipCode);
        #endregion
    }
}
