using RestWithASPNet.Data.Converter.Contract;
using RestWithASPNet.Data.VO;
using RestWithASPNet.Model;

namespace RestWithASPNet.Data.Converter.Implementation
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parser(PersonVO origem)
        {
            if (origem == null) return null;
            return new Person
            {
                Id = origem.Id,
                FirstName = origem.FirstName, 
                LastName = origem.LastName,
                Address = origem.Address,
                Gender = origem.Gender
            };
        }

        public PersonVO Parser(Person origem)
        {
            if (origem == null) return null;
            return new PersonVO
            {
                Id = origem.Id,
                FirstName = origem.FirstName,
                LastName = origem.LastName,
                Address = origem.Address,
                Gender = origem.Gender
            };
        }
        public List<Person> Parser(List<PersonVO> origem)
        {
            if (origem == null) return null;
            return origem.Select(item => Parser(item)).ToList();
        }

        public List<PersonVO> Parser(List<Person> origem)
        {
            if (origem == null) return null;
            return origem.Select(item => Parser(item)).ToList();
        }
    }
}
