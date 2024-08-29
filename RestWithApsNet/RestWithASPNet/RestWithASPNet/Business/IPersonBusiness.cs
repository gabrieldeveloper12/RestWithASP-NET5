using RestWithASPNet.Data.VO;
using RestWithASPNet.Model;

namespace RestWithASPNet.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO PersonVO);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(long id);
    }
}
