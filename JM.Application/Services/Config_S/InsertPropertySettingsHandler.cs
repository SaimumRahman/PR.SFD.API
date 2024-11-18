using AutoMapper;
using FluentValidation;
using JM.Application.Common.Generic;
using JM.Domain.Entities;
using JM.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JM.Application.Services.Config_S
{
    public class CreatePropertySettingCommand : IRequest<ResponseResult>
    {
        public string SettingName { get; set; }
        public int IsChecked { get; set; }
        public string Property_A { get; set; }

    }
    public class CreatePropertySettingCommandValidator : AbstractValidator<CreatePropertySettingCommand>
    {

        public CreatePropertySettingCommandValidator()
        {
            RuleFor(x => x.SettingName).NotEmpty().WithMessage("License number is required");
            RuleFor(x => x.Property_A).NotEmpty().WithMessage("License number is required");
            RuleFor(x => x.IsChecked).InclusiveBetween(0, 1).WithMessage("AC must be either 0 or 1");
        }
    }
    public class InsertPropertySettingsHandler : IRequestHandler<CreatePropertySettingCommand, ResponseResult>
    {
        private readonly IUnitOfWorkJM _unitOfWork;
        public InsertPropertySettingsHandler(IUnitOfWorkJM unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseResult> Handle(CreatePropertySettingCommand request, CancellationToken cancellationToken)
        {
            ResponseResult rr = new ResponseResult();

            try
            {
                var validator = new CreatePropertySettingCommandValidator();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    rr.Message = "Error: " + string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));
                    rr.StatusCode = 1000;
                    rr.IsSuccessStatus = false;
                    return rr;
                }

                PropertySetting details = new PropertySetting()
                {
                    SettingName = request.SettingName,
                    IsChecked = request.IsChecked,
                    Property_A = request.Property_A
                };
               // _unitOfWork.BeginTransaction();
                var result = await _unitOfWork.PropertySetting.InsertPropertySettings(details);
                _unitOfWork.Commit();
                if (result > 0)
                {
                    rr.Message = "Property Setting Saved Successfully";
                    rr.StatusCode = 2000;
                    rr.IsSuccessStatus = true;
                    return rr;
                }
                else
                {
                    rr.Message = "Failed!! Property Setting NOT Saved";
                    rr.StatusCode = 3000;
                    rr.IsSuccessStatus = false;
                    return rr;
                }

            }
            catch (Exception ex)
            {
              //  _unitOfWork.Rollback();
                rr.Message = "Failed!! " + ex.Message;
                rr.StatusCode = 4000;
                rr.IsSuccessStatus = false;
                return rr;
                throw;
            }
        }

    }
}
