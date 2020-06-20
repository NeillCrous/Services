using GeneralBusiness.BusinessObjects;
using GeneralData.Model.Entities;
using GeneralDTO;
using System;

namespace GeneralBusiness.BusinessProcesses
{
    public class UserProcess : BaseBusinessObject
    {
        EventLog EventLogBONonTran;

        EventLog EventLogBO;
        User UserBO;

        public UserProcess()
        {
            EventLogBONonTran = new EventLog();

            EventLogBO = new EventLog(Repo);
            UserBO = new User(Repo);
        }

        /// <summary>Create a new user.</summary>

        public users Insert(UserDTO dto)
        {
            try
            {
                Repo.BeginTransaction();
                // Create the user.
                var newEntity = UserBO.CreateNewUser(dto);
                UserBO.Insert(newEntity);

                // Log the event.
                EventLogBO.Insert($"User {newEntity.Name} {newEntity.Surname} has been created.", newEntity.Id.ToString());
                Repo.EndTransaction();

                return newEntity;
            }
            catch (Exception ex)
            {
                Repo.RollBack();
                EventLogBONonTran.Insert($"Could not create user {dto.Name}.", null);
                throw ex;
            }
        }
    }
}
