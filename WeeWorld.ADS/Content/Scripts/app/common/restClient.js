define(['require', 'knockout'], function (require, ko) {
	
	/* API Wrapper to simplify rest calls to ADS */
    var RestClient = function (context) {
	    var self = this;

	    self.context = context;
	    self.baseUrl = self.context.getSettings('baseUrl') + 'api/';
	    self.resourceUrl = self.baseUrl + self.context.resource + '/';

	    self.Get = function (id, success, error) {

	        $.ajax({
	            url: self.resourceUrl + id,
	            type: 'GET',
	            dataType: 'json',
	            beforeSend: self.SetAuthHeader,
	            success: function (data, textStatus, jqXHR) {
	                self.Success(data, textStatus, jqXHR, success);
	            },
	            error: function (jqXHR, textStatus, errorThrown) {
	                self.Error(jqXHR, textStatus, errorThrown, error);
	            }
	        });
	    };

	    self.GetAll = function (success, error) {

	        $.ajax({
	            url: self.resourceUrl,
	            type: 'GET',
	            dataType: 'json',
	            beforeSend: self.SetAuthHeader,
	            success: function (data, textStatus, jqXHR) {
	                self.Success(data, textStatus, jqXHR, success);
	            },
	            error: function (jqXHR, textStatus, errorThrown) {
	                self.Error(jqXHR, textStatus, errorThrown, error);
	            }
	        });
	    };

	    self.Create = function (data, success, error) {

	        var postdata = ko.toJSON(data);

	        $.ajax({
	            url: self.resourceUrl,
	            type: 'Post',
	            dataType: 'json',
	            contentType: "application/json",
	            data: postdata,
	            beforeSend: self.SetAuthHeader,
	            success: function (data, textStatus, jqXHR) {
	                self.Success(data, textStatus, jqXHR, success);
	            },
	            error: function (jqXHR, textStatus, errorThrown) {
	                self.Error(jqXHR, textStatus, errorThrown, error);
	            }
	        })
	    };

	    self.Update = function (data, success, error) {

	        var postdata = ko.toJSON(data);

	        $.ajax({
	            url: self.resourceUrl + data().Id,
	            type: 'Put',
	            dataType: 'json',
	            contentType: "application/json",
	            data: postdata,
	            success: success,
	            beforeSend: self.SetAuthHeader,
	            success: function (data, textStatus, jqXHR) {
	                self.Success(data, textStatus, jqXHR, success);
	            },
	            error: function (jqXHR, textStatus, errorThrown) {
	                self.Error(jqXHR, textStatus, errorThrown, error);
	            }	    
	        })
	    };

	    self.Delete = function (id, success, error) {

	        $.ajax({
	            url: self.resourceUrl + id,
	            type: 'DELETE',
	            dataType: 'json',
	            beforeSend: self.SetAuthHeader,
	            success: function (data, textStatus, jqXHR) {
	                self.Success(data, textStatus, jqXHR, success);
	            },
	            error: function (jqXHR, textStatus, errorThrown) {
	                self.Error(jqXHR, textStatus, errorThrown, error);
	            }
	        });
	    };

	    self.RefreshFromCI = function (apps, builds, success, error) {

	        if (apps != true) { 
	            apps = false; 
	        }
	        if (builds != true) { 
	            builds = false;
	        }
	        
	        $.ajax({
	            url: self.baseUrl + 'refresh?apps=' + apps + '&builds=' + builds,
	            type: 'POST',
	            dataType: 'json',
	            beforeSend: self.SetAuthHeader,
	            success: function (data, status, request) {
	                self.Success(data, status, request, success);
	            },
	            error: function (request, status, errorThrown) {
	                self.Error(request, status, errorThrown, error);
	            }
	        });
	    };
	    
        // set authentication header (using local token code)
	    self.SetAuthHeader = function (xhr) {
	        var code = window.code;
	        xhr.setRequestHeader('Authorization', code);
	    }

        // global wrapper round all success responses
	    self.Success = function (data, status, request, success) {
            // pass on to caller
	        success(data, status, request);
	    }

        // global wrapper round all error responses (logging/alerting etc)
	    self.Error = function (request, status, errorThrown, error) {
	        
	        // pass on to caller
	        if (request.status == 400) {
	            error(JSON.parse(request.responseText));
	        }
	        else {
	            console.log({ 'request': request, 'status': status, 'error': errorThrown });
            }
	    }
	}

    return RestClient;
});