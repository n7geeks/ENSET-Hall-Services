# ENSET Hall Scolarity

Extract students info from ENSET's "Attestation de Scolarité" or "Attestation d'Inscription" using optical character recognition and OpenAI's Davinci model.

## Example
Using this service, the right hand json file was extracted from the left hand "Attestation de Scolarité" image.

<table>
<tr>
<td><img src="https://s6.imgcdn.dev/rPuwl.jpg" style="height: 30rem;" alt=""></td>
<td>
<pre><code class="json" >
{
   "fullName": "BEN SADIK YOUSSEF",
    "cne": "X000000000",
    "cin": "XX00000",
    "promo": "2021",
    "major": "glsid",
    "department": "math-info",
    "diploma": "engineering",
    "formation": "init"
}
</code></pre></td>
</tr>
</table>

## API

The app has only one endpoint /analyse that takes a form-data file (it has to be of type image) in the request body and returns a JSON response if success.

## Authentication

No authentication is required to use this API.

## Resource Usage

The /analyse endpoint accepts a form-data file as the request body. The file must be a scolarity document in image format (PNG, JPEG...). The file size limit is 4 MB.

The endpoint returns a JSON object containing the following fields if the analysis is successful:

- fullName: string, the full name of the student
- cne: string, the national student number
- cin: string, the national identity card number
- promo: string, the inscription year
- major: string, the major field of study (filière)
- department: string, the department of study
- diploma: string, the diploma obtained
- formation: string, the type of formation

For example:
```json
{
  "fullName": "John Doe",
  "cne": "123456789",
  "cin": "AB123456",
  "promo": "2020",
  "major": "ii-bdcc",
  "department": "math-info",
  "diploma": "engineering",
  "formation": "init"
}
```
### Available Major fields
The major fields are always output in lowecase and initials only, and they are:
- glsid (Génie du Logiciel et des Systèmes Informatiques Distribués)
- ii-bdcc (Ingénierie Informatique : Big Data et Cloud Computing)
- sdia (Systèmes Distribués et Intelligence Artificiell)
- mrmi (Mécanique, Robotique et Matériaux Innovants)
- gmsi (Génie Mécanique des Systèmes Industriels)
- gil (Génie Industriel et Logistique)
- gecsi (Génie Electrique et Contrôle des Systèmes Industriels)
- seer (Génie Electrique option : Systèmes Electriques et Energies Renouvelables)
- eseg (Education et Sciences Economiques et de Gestion)
- fc (Finance et Comptabilité)
- aoe (Administration et Organisations des Entreprises)

### Available Departments
- math-info (Mathématiques et Informatique)
- genie-meca (Génie Mécanique)
- genie-elect (Génie Electrique)
- genie-eco (Génie Economie et Gestion)
- staic (Sciences et Techniques Administratives et Ingénierie des Compétences)

## Error Messages

If the app encounters an error, it will return a HTTP status code other than 200 OK.

If the file is missing, the app will return a 400 Bad Request status code with the following JSON response:

```json
{
	"code":	"MissingFile",
	"message": "The file is missing."
}
```

If the file is not an image, the app will return a 400 Bad Request status code with the following JSON response:

```json
{
    "code":	"InvalidFile",
    "message": "The file is not an image."
}
```

If the file size is greater than 4 MB, the app will return a 400 Bad Request status code with the following JSON response:

```json
{
    "code":	"InvalidFile",
    "message": "The file is too large."
}
```

If the OCR module couldn't extract the text from the image, the app will return a 400 Bad Request status code with the following JSON response:

```json
{
    "code":	"TextExtractionError",
    "message": "Could not extract text from the image."
}
```

If the NLP module couldn't extract the student info from parsing text, the app will return a 400 Bad Request status code with the following JSON response:

```json
{
    "code":	"TextParsingError",
    "message": "Could not parse the text."
}
```

Otherwise, the app will return a 500 Internal Server Error status code.

## Terms of Use

By using this API, you agree to respect the intellectual property rights of the owners of the scolarity documents and not to use the API for any illegal or unethical purposes. You also agree to not abuse the API by sending excessive or malicious requests that may affect its performance or availability.
