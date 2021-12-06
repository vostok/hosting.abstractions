## 0.3.7 (06-12-2021):

Added `net6.0` target.

## 0.3.6 (14-04-2021):

Added `IVostokHostShutdown` interface.

## 0.3.5 (27-11-2020):

Added `RequiresMergedConfiguration` attribute.

## 0.3.4 (01-10-2020):

CompositeApplication: bugfix (rethrow single application crash exception with captured stacktrace).

## 0.3.2 (30-09-2020):

Fixed a bug (null environment in CompositeApplication.PreInitializeAsync) introduced in 0.3.1.

## 0.3.1 (30-09-2020):

CompositeApplication: bugfix (must pass the same env with additional local shutdown token to Initialize and Run methods).

## 0.3.0 (27-06-2020):

- Added CompositeApplication and CompositeApplicationBuilder to enable creation of multi-component apps;
- RequirementDetector: switched from app types to app instances to support CompositeApplication;
- RequirementChecker: switched from app types to app instances to support CompositeApplication;
- Added diagnostic info provider abstraction;
- Added diagnostic health check abstraction;
- Host extensions can now become mutable (AsMutable extension) to enable passing of DI container in CompositeApplication;
- Added enriching extensions for IVostokHostingEnvironment (additional shutdown tokens, log customization, etc).

## 0.2.1 (14-04-2020):

RequirementDetector now also looks into a static public property named AdditionalRequirements that returns IEnumerable<Attribute>.
This allows to express requirements with attributes whose internal types depend on generic type parameters of the application.

## 0.2.0 (16-01-2020):

Added IVostokApplication and fill IVostokHostingEnvironment.

## 0.1.6 (11-09-2019):

Added Subproject to IVostokApplicationIdentity.

## 0.1.5 (04-09-2019):

Added ITracer.

## 0.1.4 (16-07-2019):

Added IVostokApplicationIdentity and IVostokApplicationMetrics.

## 0.1.2 (26-03-2019): 

Added IConfigurationProvider and IConfigurationSource to IVostokHostingEnvironment.

## 0.1.1 (15-01-2019): 

Added IHerculesSink to IVostokHostingEnvironment.

## 0.1.0 (07-11-2018): 

Initial prerelease.
