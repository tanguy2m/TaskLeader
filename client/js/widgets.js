﻿(function( $ ) {

	$.fn.addMultipleSelect = function(options) {
		$("<div></div>").appendTo(this).MultipleSelect(options);
		return this; //To be chainable
	}

	$.widget( "Taskleader.MultipleSelect", {
 
		// These options will be used as defaults
		options: {
			label: "",
			displayAll: false,
			displayImage: false,
			values: []
		},
		
		// ID of "all" checkbox element
		boxID: '',		
		// ID of span element containing all the values
		spanID: '',
		
		// Set up the widget
		_create: function() {
		
			//Insert label		
			this.element.prepend(this.options.label);
			
			//Insert "All" checkbox if required.
			if (this.options.displayAll) {
				this.boxID = $('<input type="checkbox"/>').appendTo(this.element).uniqueId().attr("id");
				$(this.element).append('<label for="'+this.boxID+'">Tous</label>');
				$("#"+this.boxID).button().click($.proxy(this._allClicked, this)); //Keep "this" linked to the widget
			}
			
			//Insert values
			var span = $('<span></span>').appendTo(this.element);
			this.spanID = span.uniqueId().attr("id");
			$.each(this.options.values,function(index,value) {
				var id = $('<input type="checkbox"/>').appendTo(span).uniqueId().attr("id");
				span.append('<label for="'+id+'">'+value+'</label>');
			});
			span.appendTo(this.element).buttonset();
			$("#"+this.spanID).click($.proxy(this._itemClicked, this));
		},
		
		// Called when "all" checkbox has been selected
		_allClicked: function() {		
			$("#"+this.spanID+" > input").prop("checked",$("#"+this.boxID).prop("checked"));
			$("#"+this.spanID).buttonset("refresh");
		},
		
		// Called when an item in the list has been selected
		_itemClicked: function() {
			if ($("#"+this.boxID).prop("checked"))
				$("#"+this.boxID).prop("checked",false).button("refresh");
		},
		
		// Use the destroy method to clean up any modifications your widget has made to the DOM
		_destroy: function() {
		}
  });
}( jQuery ) );