# UWP Release Build bug (OS 1809/17763) and VS2017 v15.9.10

# Usage:

Build, Deploy, Run.

The Debug Build exhibits correct behavior when using the Navigation Menu, the Release Build fails to call the ItemSelected() method.

Place a breakpoint inside the ItemSelected() method on the Release Build.  Reflection handling seems to be the issue.  This used to work.

## Bugs:

All bugs have been tested against Windows 10 v 1809 (RS5).
