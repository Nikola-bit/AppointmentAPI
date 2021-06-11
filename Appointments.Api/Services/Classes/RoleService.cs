using Appointments.Api.Data;
using Appointments.Api.Models;
using Appointments.Api.Utilities;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository repository;
        private readonly IMapper mapper;
        public RoleService(IRoleRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }
        public RoleDTO AddRole(RoleDTO informations)
        {
            RoleCreateDTO helper = mapper.Map<RoleCreateDTO>(informations);

            Role request = mapper.Map<Role>(helper);

            Role result = repository.AddRole(request);

            RoleDTO response = mapper.Map<RoleDTO>(result);

            return response;
        }
        public RoleDTO UpdateRole(RoleCreateDTO informations)
        {
            Role request = mapper.Map<Role>(informations);

            Role result = repository.UpdateRole(request);

            RoleDTO response = mapper.Map<RoleDTO>(result);

            return response;
        }
        public RoleDTO RoleById(string ID)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(ID));

            Role result = repository.RoleById(request);

            RoleDTO response = mapper.Map<RoleDTO>(result);

            return response;
        }
        public List<RoleDTO> ListAllRoles(RolePaginationFilter informations)
        {
            List<Role> result = repository.ListAllRoles(informations);

            List<RoleDTO> response = mapper.Map<List<RoleDTO>>(result);

            return response;
        }
        public RoleDTO DeleteRole(string ID)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(ID));

            Role result = repository.DeleteRole(request);

            RoleDTO response = mapper.Map<RoleDTO>(result);

            return response;

        }
    }
}
