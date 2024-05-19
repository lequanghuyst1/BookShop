using AutoMapper;
using BookShopOnline.Core.Dto.Vourcher;
using BookShopOnline.Core.Entitites;
using BookShopOnline.Core.Exceptions;
using BookShopOnline.Core.Helper;
using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.Services;
using BookShopOnline.Core.Resources;
using BookShopOnline.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Services
{
    public class VoucherService : BaseService<Voucher, VoucherDto>, IVoucherService
    {
        IVoucherRepository _voucherRepository;
        public VoucherService(IVoucherRepository vourcherRepository, IMapper mapper, IImageService imageService) : base(vourcherRepository, mapper, imageService)
        {
            _voucherRepository = vourcherRepository;
        }

        public async Task<VoucherDto> ApplyVoucher(string voucherCode, decimal totalProductCost)
        {
            var voucher = await _voucherRepository.GetByVoucherCode(voucherCode);
            var voucherDto = _mapper.Map<VoucherDto>(voucher);
            if (voucher != null)
            {
                if (totalProductCost < voucher.VoucherCondition)
                {
                    errors.Add("VoucherCondition", new string[] { $"Tổng đơn hàng tối thiểu {ValueFormat.GetDecimalTypeFormat(voucher.VoucherCondition)} mới có thể áp dụng." });
                    throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
                }
                else if (DateTime.Now > voucher.ExpiredDate)
                {
                    errors.Add("ExpiredDate", new string[] { $"Mã khuyến mãi đã hết hạn sử dụng." });
                    throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
                }
                else if (voucher.Quantity <= 0)
                {
                    errors.Add("Quantity", new string[] { $"Số lượt áp dụng đã hết." });
                    throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
                }
                else
                {
                    return voucherDto;
                }
            }
            else
            {
                errors.Add("VoucherCode", new string[] { $"Mã khuyến mãi không hợp lệ." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }
        }

        public override async Task ValidateBeforeInsert(Voucher voucher)
        {
            if (await _voucherRepository.CheckDuplicateCodeAsync(voucher.VoucherCode))
            {
                errors.Add("VoucherCode", new string[] { $"Mã khuyến mãi <{voucher.VoucherCode}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }
        }
        public async override Task ValidateBeforeUpdate(Voucher voucher)
        {
            if (await _voucherRepository.CheckEntityCodeUpdateAsync(voucher.VoucherCode, voucher.VoucherId))
            {
                errors.Add("VoucherCode", new string[] { $"Mã khuyến mãi <{voucher.VoucherCode}> đã tồn tại trong hệ thống." });
                throw new ValidateException(ResourceVN.Exception_Validate_Default, errors);
            }
        }
    }
}
