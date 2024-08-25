using RestWithASPNet.Model;
using RestWithASPNet.Model.Context;
using System;

namespace RestWithASPNet.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySqlContext _sqlContext;
        public PersonRepositoryImplementation(MySqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public List<Person> FindAll()
        {
            return _sqlContext.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _sqlContext.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }
        public Person Create(Person person)
        {
            try
            {
                _sqlContext.Add(person);
                _sqlContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }
        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();

            var result = _sqlContext.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            {
                try
                {
                    _sqlContext.Entry(result).CurrentValues.SetValues(person);
                    _sqlContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;

        }

        public void Delete(long id)
        {
            var result = _sqlContext.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _sqlContext.Persons.Remove(result);
                    _sqlContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(long id)
        {
            return _sqlContext.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
