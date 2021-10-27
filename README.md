# test-task-cts

- for some reason all generated trades have missing id - I am using 0 as default, when no id set
- save via trade.Create() method is throwing weird Exception randomly, I've commented saving to db and just print record to console
- there are missing tests for all components, I've created everything as independent testable classes, using interfaces for potential DI container usage