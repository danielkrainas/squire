namespace Squire.Security.Authorization
{
    using System.Collections.Generic;

    public interface IRoleResolver
    {
        /// <summary>
        /// Resolves all roles.
        /// </summary>
        /// <returns>Returns an enumeration of all roles available.</returns>
        IEnumerable<IRole> ResolveAll();

        /// <summary>
        /// Resolves all roles that match the given ids, specified by <paramref name="ids"/>.
        /// </summary>
        /// <param name="ids">Ids of the roles to resolve</param>
        /// <returns>Returns an enumeration of all roles that were matched, otherwise an empty set.</returns>
        IEnumerable<IRole> ResolveAll(params string[] ids);

        /// <summary>
        /// Resolves a role by its id, specified by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Id of the role to resolve</param>
        /// <returns>Returns the role if found, otherwise null.</returns>
        IRole Resolve(string id);

        /// <summary>
        /// Resolves a role based on a registration ticket, specified by <paramref name="registration"/>.
        /// </summary>
        /// <param name="registration">Registration ticket for the role to resolve.</param>
        /// <returns>Returns the a newly created role if the ticket was valid, otherwise null.</returns>
        IRole Resolve(RoleRegistration registration);
    }
}
