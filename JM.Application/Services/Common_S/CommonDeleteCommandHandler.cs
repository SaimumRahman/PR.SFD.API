using AutoMapper;
using FluentValidation;
using MediatR;
using JM.Domain.CommonDTO;
using JM.Infrastructure.Models;
using JM.Application.Common.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JM.Application.Services.Common_S
{
    public class CommonDeleteCommand : IRequest<ResponseResult>
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int PrimaryKey { get; set; }
        public int LoggedUserId { get; set; }
    }
    public class CommonDeleteCommandValidator : AbstractValidator<CommonDeleteCommand>
    {
        public CommonDeleteCommandValidator()
        {
            RuleFor(x => x.TableName)
                .NotEmpty().WithMessage("Customer Name cannot be empty")
                .NotNull().WithMessage("Customer Name cannot be null"); 
            RuleFor(x => x.ColumnName)
                .NotEmpty().WithMessage("Column Name cannot be empty")
                .NotNull().WithMessage("Column Name cannot be null");
            RuleFor(x => x.PrimaryKey)
                .GreaterThan(0).WithMessage("Primary Key cannot be empty")
                .NotNull().WithMessage("Primary Key cannot be empty");
            RuleFor(x => x.LoggedUserId)
                .GreaterThan(0).WithMessage("Logged User Id cannot be empty")
                .NotNull().WithMessage("Logged User Id cannot be empty");


        }
    }
    public class CommonDeleteCommandHandler : IRequestHandler<CommonDeleteCommand, ResponseResult>
    {
        private readonly IUnitOfWorkJM _unitOfWork;
        private readonly IMapper _mapper;
        public CommonDeleteCommandHandler(IUnitOfWorkJM unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseResult> Handle(CommonDeleteCommand request, CancellationToken cancellationToken)
        {
            ResponseResult rr = new ResponseResult();
            try
            {
                var validator = new CommonDeleteCommandValidator();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    rr.Message = "Error: " + string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));
                    rr.StatusCode = 1000;
                    rr.IsSuccessStatus = false;
                    return rr;
                }
                _unitOfWork.BeginTransaction();
                var result =  await _unitOfWork.Common.DeleteData(request.TableName, request.ColumnName, request.PrimaryKey,request.LoggedUserId);
                _unitOfWork.Commit();
                if (result > 0)
                {
                    rr.Message = "Data Deleted Successfully";
                    rr.StatusCode = 2000;
                    rr.IsSuccessStatus = true;
                    return rr;
                }
                else
                {
                    rr.Message = "Failed!! Data Not Deleted";
                    rr.StatusCode = 3000;
                    rr.IsSuccessStatus = false;
                    return rr;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                rr.Message = "Failed!! " + ex.Message;
                rr.StatusCode = 4000;
                rr.IsSuccessStatus = false;
                return rr;
                throw;
            }
          
        }
    }
}
