Common interfaces, such as those that were used for Event publishing, and 
abstract base classes for Entities and Value Objects:

  - `ConcurrencySafeEntity`: Class `ConcurrencySafeEntity` is a **Layer Supertype**
	[Fowler, P of EAA] used to manage surrogate identity and optimistic concurrency 
	versioning, as explained in **Entities (5)**.
  - DomainEvent
  - `DomainEventPublisher`: a lightweight component based on the **Observer pattern**
	[Gamma et al.]. See **Domain Events (8)** for details on how Events get published 
	broadly.
  - DomainEventSubscriber
  - `DomainRegistry`: uses Spring to look up references to beans that implement 
	interfaces defined by the domain model, including Repositories and Domain Services.
  - Entity
  - IdentifiedDomainObject
  - IdentifiedValueObject