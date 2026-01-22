namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;
using Comanda.Shared.Enums;

public class PersonUseCase(
    IPersonRepository personRepository) : UseCaseBase(EntityTypePrintNames.Person)
{
    private readonly IPersonRepository _personRepository = personRepository;

    public async Task<Person> CreatePersonAsync(string name)
    {
        var person = new Person(name);
        await _personRepository.AddAsync(person);
        return person;
    }

    public async Task<Person> GetPersonByPublicIdAsync(string publicId)
    {
        return await _personRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        return await _personRepository.GetAllAsync();
    }

    public async Task<Person> UpdatePersonNameAsync(string publicId, string newName)
    {
        var person = await _personRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        person.UpdateName(newName);
        await _personRepository.UpdateAsync(person);

        return person;
    }

    public async Task<Person> AddContactAsync(string personPublicId, ClientContactType type, string value)
    {
        var person = await _personRepository.GetByPublicIdAsync(personPublicId)
            ?? throw new NotFoundException(EntityTypePrintName, personPublicId);

        var contact = new PersonContact(type, value);
        person.AddContact(contact);
        await _personRepository.UpdateAsync(person);

        return person;
    }

    public async Task<Person> RemoveContactAsync(string personPublicId, string contactPublicId)
    {
        var person = await _personRepository.GetByPublicIdAsync(personPublicId)
            ?? throw new NotFoundException(EntityTypePrintName, personPublicId);

        var contact = person.Contacts.FirstOrDefault(c => c.PublicId == contactPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.PersonContact, contactPublicId);

        person.RemoveContact(contact);
        await _personRepository.UpdateAsync(person);

        return person;
    }

    public async Task DeletePersonAsync(string publicId)
    {
        var person = await _personRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _personRepository.DeleteAsync(person);
    }
}
