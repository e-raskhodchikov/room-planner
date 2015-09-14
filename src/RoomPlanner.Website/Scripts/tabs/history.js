(function(pages, urls) {

	'use strict';

	var ui = {},
		templates = {
			RoomHistory: 'roomHistoryTemplate'
		},
		uiContainer = '',
		loadDataXhr = null;

	function bindUi() {
		ui = {
			isShortCheckbox: $(uiContainer + ' [data-control="isShortControl"]'),
			historyPlaceholder: $(uiContainer + ' [data-control="historyPlaceholder"]'),
			loader: $(uiContainer + ' [data-control="loader"]')
		};
	}

	function renderData(data) {
		var html = $.render[templates.RoomHistory](data || []);
		ui.historyPlaceholder.html(html);
	}

	function stopLoadData() {
		loadDataXhr && loadDataXhr.abort();
		ui.loader.hide();
	}

	function loadData() {
		stopLoadData();
		renderData();

		ui.loader.show();
		loadDataXhr = $.post(urls.GetPlansHistory,
			{
				isShort: ui.isShortCheckbox.prop('checked')
			},
			function(data) {
				loadDataXhr = null;
				stopLoadData();
				renderData(data);
			});
	}

	function bindEvents() {
		ui.isShortCheckbox.on('change', loadData);
	}

	function bindTemplates() {
		$.each(templates, function (index, template) {
			$.templates(template, '#' + template);
		});
	}

	function initialize(initUiContainer) {
		uiContainer = initUiContainer;
		bindUi();
		bindEvents();
		bindTemplates();
	}

	pages.History = {
		initialize: initialize,
		stopLoadData: stopLoadData,
		loadData: loadData
	};

})(App.Tabs, App.Urls);
