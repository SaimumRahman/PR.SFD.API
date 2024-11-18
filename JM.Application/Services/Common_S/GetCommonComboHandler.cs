using AutoMapper;
using MediatR;
using JM.Domain.CommonDTO;
using JM.Application.Common.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JM.Application.Services.Common_S
{
    public class GetCommonComboQuery : IRequest<IEnumerable<CommonComboDto>>
    {
        public string TableName { get; set; }
        public string ValueField { get; set; }
        public string TextField { get; set; }
        public string CascadingField { get; set; }
        public string CascadingValue { get; set; }


    }
    public class GetCommonComboHandlerQuery : IRequestHandler<GetCommonComboQuery, IEnumerable<CommonComboDto>>
    {
        private readonly IUnitOfWorkJM _unitOfWork;
        public GetCommonComboHandlerQuery(IUnitOfWorkJM unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CommonComboDto>> Handle(GetCommonComboQuery request, CancellationToken cancellationToken)
        {
            string commaSepartedString = $"{request.ValueField},{request.TextField},{request.TableName}";
            if (!string.IsNullOrEmpty(request.CascadingField))
            {
                commaSepartedString += $",{request.CascadingField},{request.CascadingValue}";
            }

            return await _unitOfWork.Common.GetCommonComboData(commaSepartedString);
        }
    }
}
