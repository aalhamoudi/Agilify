# Data Sync

![](Domain-Model.jpeg)

Each of entities depicted above is associated with table controller in the backend, that the client sync individually, and from which it contructs an entity graph (i.e. team object would have a collection of projects/members, a project would have a collection of epics/sprints and so on)

When the data is updated on the client, it syncs individual tables through the associated controller, or performs bulk operations via sending top level entity graph (e.g. Team), and the backend handles updating individial tables for efficiency.