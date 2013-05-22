﻿using System;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// A Client for the GitHub API v3. You can read more about the api here: http://developer.github.com.
    /// </summary>
    public class GitHubClient : IGitHubClient
    {
        /// <summary>
        /// Create a new instance of the GitHub API v3 client pointing to 
        /// https://api.github.com/
        /// </summary>
        public GitHubClient(string userAgent) : this(new Connection(userAgent))
        {
        }

        public GitHubClient(string userAgent, ICredentialStore credentialStore) : this(new Connection(userAgent, credentialStore))
        {
        }

        public GitHubClient(string userAgent, Uri baseAddress) : this(new Connection(userAgent, baseAddress))
        {
        }

        public GitHubClient(string userAgent, ICredentialStore credentialStore, Uri baseAddress)
            : this(new Connection(userAgent, baseAddress, credentialStore))
        {
        }

        public GitHubClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            Connection = connection;
            Authorization = new AuthorizationsClient(new ApiConnection(connection));
            Miscellaneous = new MiscellaneousClient(connection);
            Notification = new NotificationsClient(new ApiConnection(connection));
            Organization = new OrganizationsClient(new ApiConnection(connection));
            Repository = new RepositoriesClient(new ApiConnection(connection));
            Release = new ReleasesClient(new ApiConnection(connection));
            User = new UsersClient(new ApiConnection(connection));
            SshKey = new SshKeysClient(new ApiConnection(connection));
        }

        /// <summary>
        /// Convenience property for getting and setting credentials.
        /// </summary>
        /// <remarks>
        /// You can use this property if you only have a single hard-coded credential. Otherwise, pass in an 
        /// <see cref="ICredentialStore"/> to the constructor. 
        /// Setting this property will change the <see cref="ICredentialStore"/> to use 
        /// the default <see cref="InMemoryCredentialStore"/> with just these credentials.
        /// </remarks>
        public Credentials Credentials
        {
            get { return Connection.Credentials; }
            // Note this is for convenience. We probably shouldn't allow this to be mutable.
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                Connection.Credentials = value;
            }
        }

        /// <summary>
        /// The base address of the GitHub API. This defaults to https://api.github.com,
        /// but you can change it if needed (to talk to a GitHub:Enterprise server for instance).
        /// </summary>
        public Uri BaseAddress
        {
            get { return Connection.BaseAddress; }
        }

        /// <summary>
        /// Provides a client connection to make rest requests to HTTP endpoints.
        /// </summary>
        public IConnection Connection { get; private set; }

        public IAuthorizationsClient Authorization { get; private set; }
        public IMiscellaneousClient Miscellaneous { get; private set; }
        public IOrganizationsClient Organization { get; private set; }
        public IRepositoriesClient Repository { get; private set; }
        public IReleasesClient Release { get; private set; }
        public ISshKeysClient SshKey { get; private set; }
        public IUsersClient User { get; private set; }
        public INotificationsClient Notification { get; private set; }
    }
}
