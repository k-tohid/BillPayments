using BillPayments.Application.DTOs.CategoryDTO;
using BillPayments.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.Mappings
{
    public static class CategoryMapper
    {
        public static ReadCategoryDTO ToReadCategoryDTO(this Category category)
        {
            return new ReadCategoryDTO
            {
                Id = category.Id,
                Title = category.Title,
                IsActive = category.IsActive,
                Thumbnail = category.Thumbnail,
                ParentId = category.ParentId,
                SubCategories = category.SubCategories?.Select(sc => sc.ToReadCategoryDTO()).ToList()

            };
        }

        public static IEnumerable<ReadCategoryDTO> ToReadCategoryDTO(this IEnumerable<Category> categories)
        {
            return categories.Select(category => ToReadCategoryDTO(category));
        }

    }
}
