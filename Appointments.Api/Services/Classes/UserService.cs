using Appointments.Api.Data;
using Appointments.Api.Filters;
using Appointments.Api.Models;
using Appointments.Api.Services;
using Appointments.Api.Utilities;
using Appointments.API.Data;
using AppointmentsDTO.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services
{
    public class UserService : IUserService
    {
        public IMapper mapper { get; set; }
        public IUserRepository repository { get; set; }
        public IAuthRepository authRepository { get; set; }
        public UserService(IMapper _mapper, IUserRepository _repository, IAuthRepository _authRepository)
        {
            mapper = _mapper;
            repository = _repository;
            authRepository = _authRepository;
        }
        public UserRoleDTO CreateUser(UserDTO informations)
        {
            UserCreateDTO helper = mapper.Map<UserCreateDTO>(informations);

            User request = mapper.Map<User>(helper);

            User result = repository.CreateUser(request);

            UserRoleDTO response = mapper.Map<UserRoleDTO>(result);

            return response;
        }
        public UserRoleDTO UpdateUser(UserDTO informations)
        {
            User request = mapper.Map<User>(informations);

            User result = repository.UpdateUser(request);

            UserRoleDTO response = mapper.Map<UserRoleDTO>(result);

            return response;
        }
        public UserRoleDTO FindByID(string ID)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(ID));

            User result = repository.FindByID(request);

            UserRoleDTO response = mapper.Map<UserRoleDTO>(result);

            return response;
        }
        public UserRoleDTO DeleteByID(string ID)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(ID));

            User result = repository.DeleteByID(request);

            UserRoleDTO response = mapper.Map<UserRoleDTO>(result);

            return response;

        }
        public List<UserRoleDTO> ListAllUsers(UserPaginationFilter informations)
        {
            List<User> result = repository.ListAllUsers(informations);

            List<UserRoleDTO> response = mapper.Map<List<UserRoleDTO>>(result);

            return response;
        }
        public UserToken Login(UserLogin informations)
        {
            informations.Email = DataEncryption.Encrypt(informations.Email);
            informations.Password = DataEncryption.Encrypt(informations.Password);
            User result = repository.Login(informations);

            if (result != null)
            {
                UserToken response = new UserToken();

                response.User = mapper.Map<UserRoleDTO>(result);

                response.Token = authRepository.CreateToken(result);

                return response;
            }

            else return null;
        }
    }
}
