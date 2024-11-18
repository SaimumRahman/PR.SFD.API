using AutoMapper;
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

namespace JM.Application.Services
{
    public class CreateBankCommand : IRequest<ResponseResult>
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public bool IsActive { get; set; }
    }
    public class CreateBankHandler : IRequestHandler<CreateBankCommand, ResponseResult>
    {
        private readonly IUnitOfWorkJM _unitOfWork;
        public CreateBankHandler(IUnitOfWorkJM unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseResult> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
            ResponseResult rr = new();
            try
            {
                Bank bank = new Bank()
                {
                    BankName=request.BankName,
                    IsActive=request.IsActive,
                };
                await _unitOfWork.BankRepository.Add(bank);
                var reuslt = await _unitOfWork.SaveChangesAsync();
                rr.Id = reuslt.ToString();
                rr.Message = "Bank has been saved successfully";
                rr.IsSuccessStatus = true;

            }
            catch (Exception ex)
            {
                rr.IsSuccessStatus = false;
                rr.Message = ex.Message;
            }
            return await Task.Run(() => rr);
        }
    }
}
