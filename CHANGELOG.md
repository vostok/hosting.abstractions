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
