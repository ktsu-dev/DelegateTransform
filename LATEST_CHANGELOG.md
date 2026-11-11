## v3.0.0 (major)

Changes since v2.0.0:

- Drop FuncRef so an expression-bodied ref lambda keeps its change [major] (#92) (matt-edmondson)
- Add CompatibilitySuppressions.xml for the Polyfill 11.4.5 shim drift [patch] (#91) (matt-edmondson)
- Suppress S3236 for the explicit nameof(@delegate) argument (Claude)
- Name the delegate parameter without its verbatim @ in ArgumentNullException [patch] (Claude)
- Join the deps.json path rather than combining it (Claude)
- Drop the unused ktsu.ScopedAction package reference [patch] (Claude)

