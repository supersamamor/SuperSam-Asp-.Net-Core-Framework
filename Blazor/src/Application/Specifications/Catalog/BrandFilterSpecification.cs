using ProjectNamePlaceHolder.Application.Specifications.Base;
using ProjectNamePlaceHolder.Domain.Entities.Catalog;

namespace ProjectNamePlaceHolder.Application.Specifications.Catalog
{
    public class BrandFilterSpecification : HeroSpecification<Brand>
    {
        public BrandFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
