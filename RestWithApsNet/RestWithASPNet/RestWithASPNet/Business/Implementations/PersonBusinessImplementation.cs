using RestWithASPNet.Data.Converter.Implementation;
using RestWithASPNet.Data.VO;
using RestWithASPNet.Model;
using RestWithASPNet.Repository.Generic;

namespace RestWithASPNet.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        private readonly PersonConverter _converter;
        public PersonBusinessImplementation(IRepository<Person> repos, PersonConverter converter)
        {
            _repository = repos;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parser(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parser(_repository.FindById(id));
        }
        public PersonVO Create(PersonVO PersonVO)
        {
            var personEntity = _converter.Parser(PersonVO);
            personEntity = _repository.Create(personEntity);
            return _converter.Parser(personEntity);
        }
        public PersonVO Update(PersonVO PersonVO)
        {
            var personEntity = _converter.Parser(PersonVO);
            personEntity = _repository.Update(personEntity);
            return _converter.Parser(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);

        }
    }
}
