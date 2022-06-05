# Module Swapper

Sample prototype PRISM WPF application which detects diferent versions of the same module at runtime.
(This is just a first version sample to test out some ideas)

e.g:
ModuleA v1

And at runtime we want to swap it for:
ModuleA v2


How to test:

The folder SampleDLLs has the two versions of the same module:
ModuleA v1
ModuleA v2


ModulesOutput: Folder which the module catalog is looking to detect new modules.

To test:
- We drop in ModulesOutput one module and see how the regions change.
- We then drop the new version of the module and see how the regions change again.
