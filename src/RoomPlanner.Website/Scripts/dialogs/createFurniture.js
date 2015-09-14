(function(module, urls) {

	'use strict';

	var $ui,
		$uiContainer,
		dialog,
		options;

	function bindUi() {
		$ui = {
			furnitureName: dialog.find('[data-control="furnitureName"]')
		};
	}

	function createFurniture() {
		var furnitureName = $ui.furnitureName.val();
		if (!furnitureName || furnitureName.length === 0) {
			$ui.furnitureName.focus();
			return;
		}

		$.post(urls.CreateFurniture,
		{
			roomName: options.roomName,
			date: options.date,
			furnitureName: furnitureName
		}, function() {
			dialog.dialog('close');
			options.callback();
		});
	}

	function open(initOptions) {
		$uiContainer = $('[data-control="createFurnitureDialog"]');

		options = initOptions;

		dialog = $uiContainer.dialog({
			autoOpen: true,
			modal: true,
			buttons: {
				'Ок': createFurniture,
				'Отмена': function() {
					dialog.dialog('close');
				}
			},
			close: function() {
				$ui.furnitureName.val('');
			}
		});

		bindUi();

		$ui.furnitureName.autocomplete({
			source: options.allFurniture
		});
	}

	module.CreateFurniture = {
		open: open
	};

})(App.Dialogs, App.Urls);