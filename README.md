# What is ASP.NET CORE - BRAV?

BRAV is an acronym for Boostrap, Razor, Ajax, Validate. It is a simple project for educational purposes and uses:

- ASP.NET Core MVC
- Boostrap 4
- Razor Pages and Razor Ajax
- Data Annotation
- Jquery, Jquery Validate and Jquery Ajax Unobtrusive
- Auto Mapper
- EFCore - Database in memory.

#### JQuery Unobtrusive AJAX with Razor Page

```razor
<form id="formCreate"
	name="formCreate"
	method="post"
	data-ajax-url="Student/Create"
	data-ajax="true"
	data-ajax-method="post"
	data-ajax-begin="onBegin"
	data-ajax-success="onSuccess"
	data-ajax-failure="onFailure"
	data-ajax-complete="onComplete"
	data-ajax-loading=".modal-spinner">
	
  <!-- form Inputs and button -->...

</form>
```


#### Jquery Unobtrusive Parse

After rendering the modal content, this code: `$.validator.unobtrusive.parse('form-id')`, enables Jquery Validate and JQuery Unobtrusive for an MVC form.

```javascript
function enableFormValidationAfterAjaxPage(elementSelector) {
  const validator = $(elementSelector).validate();
	if (validator) {
		validator.destroy();
		$.validator.unobtrusive.parse(elementSelector);
	}
};
```

See a demo at [https://brav.azurewebsites.net](https://brav.azurewebsites.net/)
