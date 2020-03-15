using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkflowProcess.Data;
using WorkflowProcess.Models;
using WorkflowProcess.ViewModels;

namespace WorkflowProcess.MapperConfig
{
    public class AutoMapperConfiguration
    {
        public static void Config()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SubMenuMasterCreate, SubMenuMaster>()
                    .ForMember(dest => dest.SubMenuName, opt => opt.MapFrom(src => src.SubMenuName))
                    .ForMember(dest => dest.ActionMethod, opt => opt.MapFrom(src => src.ActionMethod))
                    .ForMember(dest => dest.ControllerName, opt => opt.MapFrom(src => src.ControllerName))
                    .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId))
                    .ForMember(dest => dest.SubMenuId, opt => opt.MapFrom(src => src.SubMenuId))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                    .ForMember(dest => dest.UserId, opt => opt.Ignore());

                cfg.CreateMap<SubMenuMaster, SubMenuMasterViewModel>();
                cfg.CreateMap<UsermasterView, Usermaster>();
                cfg.CreateMap<AssignRoleViewModel, SavedMenuRoles>();
                cfg.CreateMap<AssignRoleViewModelSubMenu, SavedSubMenuRoles>();
                cfg.CreateMap<SavedAssignedRoles, AssignViewUserRoleModel>();

                cfg.CreateMap<CreateUserViewModel, Usermaster>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                    .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                    .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmailId))
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                    .ForMember(dest => dest.UserId, opt => opt.Ignore());

                cfg.CreateMap<CustomerViewModel, Customers>()
                   .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                   .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src => src.CustomerAddress))
                   .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                   .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                   .ForMember(dest => dest.Status, opt => opt.Ignore())
                   .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                   .ForMember(dest => dest.CustomerID, opt => opt.Ignore());

                cfg.CreateMap<CustomerViewModel, Customer>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src => src.CustomerAddress))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore());

                cfg.CreateMap<ProjectModel, Project>()
               .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.ProjectName))
               .ForMember(dest => dest.ProjectStartDate, opt => opt.MapFrom(src => src.ProjectStartDate))
               .ForMember(dest => dest.ProjectEndDate, opt => opt.MapFrom(src => src.ProjectEndDate))
               .ForMember(dest => dest.ProjectStatusId, opt => opt.MapFrom(src => src.ProjectStatusId))
               .ForMember(dest => dest.WorkflowActivityID, opt => opt.MapFrom(src => src.WorkflowActivityID))
               .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

                cfg.CreateMap<WorkFlowModel, WorkFlow>()
              .ForMember(dest => dest.WorkflowName, opt => opt.MapFrom(src => src.WorkflowName))
              .ForMember(dest => dest.WorkflowDescription, opt => opt.MapFrom(src => src.WorkflowDescription))
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.WorkflowID, opt => opt.MapFrom(src => src.WorkflowID))
              .ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => src.CustomerID));
                //.ForMember(dest => dest.WorkflowID, opt => opt.Ignore());

                cfg.CreateMap<WorkflowActivityStreamModel, WorkflowActivityStream>()
             .ForMember(dest => dest.WorkflowActivityStreamID, opt => opt.MapFrom(src => src.WorkflowActivityStreamID))
             .ForMember(dest => dest.ActivityID, opt => opt.MapFrom(src => src.ActivityID))
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.JumpTo, opt => opt.MapFrom(src => src.JumpTo))
               .ForMember(dest => dest.Predecessor, opt => opt.MapFrom(src => src.Predecessor))
             .ForMember(dest => dest.Successor, opt => opt.MapFrom(src => src.Successor));

                cfg.CreateMap<ProjectActivityModel, ProjectActivity>()
              .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.ActivityId))
              .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
              .ForMember(dest => dest.ActionedBy, opt => opt.MapFrom(src => src.ActionedBy))
              .ForMember(dest => dest.ActionedOn, opt => opt.MapFrom(src => src.ActionedOn))
              .ForMember(dest => dest.ActivityStatusId, opt => opt.MapFrom(src => src.ActivityStatusId))
               .ForMember(dest => dest.ProjectActivityId, opt => opt.MapFrom(src => src.ProjectActivityId));

                cfg.CreateMap<ActivityModel, Activity>()
              //.ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.ActivityId))
              .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.ActivityName))
              .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
               .ForMember(dest => dest.ActivityDescription, opt => opt.MapFrom(src => src.ActivityDescription))
               .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.ActivityId));
                ;
            });
        }
    }
}