# Documentation for Content Delivery Management API

<a name="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints
All URIs are relative to *https://services.unity.com*
Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*BadgesApi* | [**DeleteBadge**](Apis/BadgesApi.md#deletebadge) | **DELETE** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/badges/{badgename} | Delete a badge
*BadgesApi* | [**GetBadge**](Apis/BadgesApi.md#getbadge) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/badges/{badgename} | Get badge
*BadgesApi* | [**ListBadges**](Apis/BadgesApi.md#listbadges) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/badges | Get badges
*BadgesApi* | [**UpdateBadge**](Apis/BadgesApi.md#updatebadge) | **PUT** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/badges | Assign a badge
*BucketsApi* | [**CreateBucketByProject**](Apis/BucketsApi.md#createbucketbyproject) | **POST** /api/ccd/management/v1/projects/{projectid}/buckets | Create bucket
*BucketsApi* | [**DeleteBucket**](Apis/BucketsApi.md#deletebucket) | **DELETE** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid} | Delete a bucket
*BucketsApi* | [**GetBucket**](Apis/BucketsApi.md#getbucket) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid} | Get a bucket
*BucketsApi* | [**GetDiff**](Apis/BucketsApi.md#getdiff) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/diff/unreleased | Get counts of changes since last release
*BucketsApi* | [**GetDiffEntries**](Apis/BucketsApi.md#getdiffentries) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/diff/unreleased/entries | Get changed entries since last releases
*BucketsApi* | [**ListBucketsByProject**](Apis/BucketsApi.md#listbucketsbyproject) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets | Get buckets for project
*BucketsApi* | [**PromoteBucket**](Apis/BucketsApi.md#promotebucket) | **POST** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/promote | Promote release between buckets
*BucketsApi* | [**UpdateBucket**](Apis/BucketsApi.md#updatebucket) | **PUT** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid} | Update a bucket
*ContentApi* | [**CreateContent**](Apis/ContentApi.md#createcontent) | **POST** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}/content | Create content upload for TUS
*ContentApi* | [**GetContent**](Apis/ContentApi.md#getcontent) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}/content | Get content by entryid
*ContentApi* | [**GetContentStatus**](Apis/ContentApi.md#getcontentstatus) | **HEAD** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}/content | Get content status by entryid
*ContentApi* | [**GetContentStatusVersion**](Apis/ContentApi.md#getcontentstatusversion) | **HEAD** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}/versions/{versionid}/content | Get content status for version of entry
*ContentApi* | [**GetContentVersion**](Apis/ContentApi.md#getcontentversion) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}/versions/{versionid}/content | Get content for version of entry
*ContentApi* | [**UploadContent**](Apis/ContentApi.md#uploadcontent) | **PATCH** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}/content | Upload content for entry
*DefaultApi* | [**ApiCcdManagementV1CliGet**](Apis/DefaultApi.md#apiccdmanagementv1cliget) | **GET** /api/ccd/management/v1/cli | Cli Download
*EntriesApi* | [**CreateEntry**](Apis/EntriesApi.md#createentry) | **POST** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries | Create entry
*EntriesApi* | [**CreateOrUpdateEntryByPath**](Apis/EntriesApi.md#createorupdateentrybypath) | **POST** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entry_by_path | Create or update entry by path
*EntriesApi* | [**DeleteEntry**](Apis/EntriesApi.md#deleteentry) | **DELETE** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid} | Delete entry
*EntriesApi* | [**GetEntries**](Apis/EntriesApi.md#getentries) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries | Get entries for bucket
*EntriesApi* | [**GetEntry**](Apis/EntriesApi.md#getentry) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid} | Get entry
*EntriesApi* | [**GetEntryByPath**](Apis/EntriesApi.md#getentrybypath) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entry_by_path | Get entry by path
*EntriesApi* | [**GetEntryVersion**](Apis/EntriesApi.md#getentryversion) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}/versions/{versionid} | Get entry version
*EntriesApi* | [**GetEntryVersions**](Apis/EntriesApi.md#getentryversions) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}/versions | Get entry versions
*EntriesApi* | [**UpdateEntry**](Apis/EntriesApi.md#updateentry) | **PUT** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid} | Update entry
*EntriesApi* | [**UpdateEntryByPath**](Apis/EntriesApi.md#updateentrybypath) | **PUT** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entry_by_path | Update entry by path
*OrgsApi* | [**GetOrg**](Apis/OrgsApi.md#getorg) | **GET** /api/ccd/management/v1/organizations/{orgid} | Gets organization details.
*OrgsApi* | [**GetOrgUsage**](Apis/OrgsApi.md#getorgusage) | **GET** /api/ccd/management/v1/organizations/{orgid}/usage | Gets organization Usage Details.
*OrgsApi* | [**SaveTosAccepted**](Apis/OrgsApi.md#savetosaccepted) | **PUT** /api/ccd/management/v1/organizations/{orgid} | Update tos accepted on a organization
*PermissionsApi* | [**CreatePermissionByBucket**](Apis/PermissionsApi.md#createpermissionbybucket) | **POST** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/permissions | Create a permission
*PermissionsApi* | [**DeletePermissionByBucket**](Apis/PermissionsApi.md#deletepermissionbybucket) | **DELETE** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/permissions | delete a permission
*PermissionsApi* | [**GetAllByBucket**](Apis/PermissionsApi.md#getallbybucket) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/permissions | Get permissions for bucket
*PermissionsApi* | [**UpdatePermissionByBucket**](Apis/PermissionsApi.md#updatepermissionbybucket) | **PUT** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/permissions | Update a permission
*ReleasesApi* | [**CreateRelease**](Apis/ReleasesApi.md#createrelease) | **POST** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/releases | Create release
*ReleasesApi* | [**GetRelease**](Apis/ReleasesApi.md#getrelease) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/releases/{releaseid} | Get release
*ReleasesApi* | [**GetReleaseByBadge**](Apis/ReleasesApi.md#getreleasebybadge) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/release_by_badge/{badgename} | Get release by badge
*ReleasesApi* | [**GetReleaseDiff**](Apis/ReleasesApi.md#getreleasediff) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/diff/releases | Get counts of changes between releases
*ReleasesApi* | [**GetReleaseDiffEntries**](Apis/ReleasesApi.md#getreleasediffentries) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/diff/releases/entries | Get changed entries between releases
*ReleasesApi* | [**GetReleaseEntries**](Apis/ReleasesApi.md#getreleaseentries) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/releases/{releaseid}/entries | Get release entries
*ReleasesApi* | [**GetReleaseEntriesByBadge**](Apis/ReleasesApi.md#getreleaseentriesbybadge) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/release_by_badge/{badgename}/entries | Get badged release entries
*ReleasesApi* | [**GetReleases**](Apis/ReleasesApi.md#getreleases) | **GET** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/releases | Get releases for bucket
*ReleasesApi* | [**UpdateRelease**](Apis/ReleasesApi.md#updaterelease) | **PUT** /api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/releases/{releaseid} | Update release
*UsersApi* | [**GetUserApiKey**](Apis/UsersApi.md#getuserapikey) | **GET** /api/ccd/management/v1/users/{userid}/apikey | Get user API key
*UsersApi* | [**GetUserInfo**](Apis/UsersApi.md#getuserinfo) | **GET** /api/ccd/management/v1/users/{userid} | Get user info
*UsersApi* | [**RegenerateUserApiKey**](Apis/UsersApi.md#regenerateuserapikey) | **POST** /api/ccd/management/v1/users/{userid}/apikey | Re-generate user API key
    
<a name="documentation-for-models"></a>
## Documentation for Models
 - [Models.AuthenticationError](Models/AuthenticationError.md)
 - [Models.AuthorizationError](Models/AuthorizationError.md)
 - [Models.CcdBadge](Models/CcdBadge.md)
 - [Models.CcdBadgeAssign](Models/CcdBadgeAssign.md)
 - [Models.CcdBucket](Models/CcdBucket.md)
 - [Models.CcdBucketAttributes](Models/CcdBucketAttributes.md)
 - [Models.CcdBucketCreate](Models/CcdBucketCreate.md)
 - [Models.CcdBucketPermissions](Models/CcdBucketPermissions.md)
 - [Models.CcdBucketUpdate](Models/CcdBucketUpdate.md)
 - [Models.CcdChangecount](Models/CcdChangecount.md)
 - [Models.CcdEntry](Models/CcdEntry.md)
 - [Models.CcdEntryCreate](Models/CcdEntryCreate.md)
 - [Models.CcdEntryCreateByPath](Models/CcdEntryCreateByPath.md)
 - [Models.CcdEntryUpdate](Models/CcdEntryUpdate.md)
 - [Models.CcdErrorCodes](Models/CcdErrorCodes.md)
 - [Models.CcdHttperror](Models/CcdHttperror.md)
 - [Models.CcdOrg](Models/CcdOrg.md)
 - [Models.CcdOrgTosUpdate](Models/CcdOrgTosUpdate.md)
 - [Models.CcdOrgusage](Models/CcdOrgusage.md)
 - [Models.CcdPermission](Models/CcdPermission.md)
 - [Models.CcdPermissionCreate](Models/CcdPermissionCreate.md)
 - [Models.CcdPermissionUpdate](Models/CcdPermissionUpdate.md)
 - [Models.CcdPromotebucket](Models/CcdPromotebucket.md)
 - [Models.CcdRelease](Models/CcdRelease.md)
 - [Models.CcdReleaseCreate](Models/CcdReleaseCreate.md)
 - [Models.CcdReleaseUpdate](Models/CcdReleaseUpdate.md)
 - [Models.CcdReleaseentry](Models/CcdReleaseentry.md)
 - [Models.CcdReleaseentryCreate](Models/CcdReleaseentryCreate.md)
 - [Models.CcdUsage](Models/CcdUsage.md)
 - [Models.CcdUser](Models/CcdUser.md)
 - [Models.CcdUserapikey](Models/CcdUserapikey.md)
 - [Models.CcdVersion](Models/CcdVersion.md)
 - [Models.ConflictError](Models/ConflictError.md)
 - [Models.GatewayTimeoutError](Models/GatewayTimeoutError.md)
 - [Models.InlineObject](Models/InlineObject.md)
 - [Models.InternalServerError](Models/InternalServerError.md)
 - [Models.MethodNotAllowedError](Models/MethodNotAllowedError.md)
 - [Models.NotFoundError](Models/NotFoundError.md)
 - [Models.PayloadTooLargeError](Models/PayloadTooLargeError.md)
 - [Models.QuotaExceededError](Models/QuotaExceededError.md)
 - [Models.RatelimitExceededError](Models/RatelimitExceededError.md)
 - [Models.ServiceUnavailableError](Models/ServiceUnavailableError.md)
 - [Models.TooManyRequestsError](Models/TooManyRequestsError.md)
 - [Models.ValidationError](Models/ValidationError.md)
        
<a name="documentation-for-authorization"></a>
## Documentation for Authorization
Within the `CCDManagementAPIService` object, there is a method called `SetConfigurationAuthHeader`. This method, when given the `CloudProjectSettings.accessToken`, will set the Unity Services Gateway Authentication header for the API Service so that subsequent calls will then be authenticated.
