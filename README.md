# fdn

This is an automation tool for managing customized dictionaries for flypy that could be easily shared between flypy ime and its rime mobile implementation.

## Usage

### Composing your own dictionary with YAML

+ Create a yaml file, eg. `mydict.yaml`, with the syntax of `<code>: <candidate>`

```
wtma: 我™
ntma: 你™
...
```

+ To assign multiple candidates to one code, use array syntax:

```
ttm: [他™, 它™]
```

or

```
ttm:
  - 他™
  - 它™
```

+ To set the candidate prior to the original ones, use `$` prefix:

```
xmrf: $仙人
```

+ To apply complexed priority manipulation, add `$` prefix before the code, then list all of the existing ones and the additional ones with the desired sequence, and mark the additional candidates with `+` prefix:

```
$uili: [实力, +实例, 势力]
```

### Converting to Duoduo format

```
fdn mydict.yaml -d 
```

This would emit `mydict.txt`

### Converting to Rime format

```
fdn mydict.yaml -r 
```

This would emit `flypy_user.txt` and `flypy_top.txt`

## Build & Publish

Development and building does require .NET SDK 6.0, but to get rid of runtime dependencies, publish it with AOT compilation to emit an independent executable:

```
dotnet publish -r <runtime> -c Release
```

where `<runtime>` could be something like `win-x64` or `linux-x64`. Note that a native linker must be installed globally.
