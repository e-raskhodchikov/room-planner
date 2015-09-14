(function(module, urls) {

	'use strict';

	var $ui,
		$uiContainer,
		dialog,
		options;

	function bindUi() {
		$ui = {
			roomName: dialog.find('[data-control="roomName"]')
		};
	}

	function createRoom() {
		var roomName = $ui.roomName.val();
		if (!roomName || roomName.length === 0) {
			$ui.roomName.focus();
			return;
		}

		$.post(urls.CreateRoom,
		{
			roomName: roomName,
			date: options.date

		}, function() {
			dialog.dialog('close');
			options.callback();
		});
	}

	function open(initOptions) {
		$uiContainer = $('[data-control="createRoomDialog"]');

		options = initOptions;

		dialog = $uiContainer.dialog({
			autoOpen: true,
			modal: true,
			buttons: {
				'Ок': createRoom,
				'Отмена': function() {
					dialog.dialog('close');
				}
			},
			close: function () {
				$ui.roomName.val('');
			}
		});

		bindUi();
	}

	module.CreateRoom = {
		open: open
	};

})(App.Dialogs, App.Urls);