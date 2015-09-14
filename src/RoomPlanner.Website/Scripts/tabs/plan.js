(function(tabs, urls, dialogs) {

	'use strict';

	var $ui,
		templates = {
			RoomPlan: 'roomPlanTemplate'
		},
		$uiContainer,
		loadDataXhr;

	function bindUi() {
		$ui = {
			planDate: $uiContainer.find('[data-control="planDate"]'),
			content: $uiContainer.find('[data-control="content"]'),
			planPlaceholder: $uiContainer.find('[data-control="planPlaceholder"]'),
			loader: $uiContainer.find('[data-control="loader"]')
		};
	}

	function renderData(data) {
		var html = $.render[templates.RoomPlan](data || []);
		$ui.planPlaceholder.html(html);
	}

	function stopLoadData() {
		loadDataXhr && loadDataXhr.abort();
		$ui.content.show();
		$ui.loader.hide();
	}

	function loadData() {
		stopLoadData();
		renderData();

		$ui.content.hide();
		$ui.loader.show();
		loadDataXhr = $.post(urls.GetRoomPlan,
			{
				date: $ui.planDate.val()
			},
			function(data) {
				loadDataXhr = null;
				stopLoadData();
				renderData(data);
			});
	}

	function initializeUi() {
		$ui.planDate.datepicker({
			changeMonth: true,
			changeYear: true,
			showButtonPanel: true,
			dateFormat: 'yy-mm-dd',
			onSelect: loadData
		});
	}

	function getRoomNameEl(target) {
		return $(target).closest('[data-control="room"]').find('[data-bind="RoomName"]');
	}

	function getFurnitureNameEl(target) {
		return $(target).closest('[data-control="furniture"]').find('[data-bind="FurnitureName"]');
	}

	function getAllFurniture() {
		return _.chain($uiContainer.find('[data-bind="FurnitureName"]'))
			.map(function(x) {
				return $(x).html(); })
			.uniq()
			.value();
	}

	function getFurnitureCountEl(target) {
		return $(target).closest('[data-control="furniture"]').find('[data-bind="FurnitureCount"]');
	}

	function removeRoom(event) {
		var roomName = getRoomNameEl(event.target);
		console.log(roomName.html());
	}

	function getDate() {
		return $ui.planDate.val();
	}

	function addFurniture(event) {
		$.post(urls.CreateFurniture,
		{
			roomName: getRoomNameEl(event.target).html(),
			date: getDate(),
			furnitureName: getFurnitureNameEl(event.target).html()
		}, function() {
			loadData();
		});
	}

	function moveFurniture(event) {
		var roomName = getRoomNameEl(event.target),
			furnitureName = getFurnitureNameEl(event.target),
			furnitureCount = getFurnitureCountEl(event.target);

		console.log(roomName.html());
		console.log(furnitureName.html());
		console.log(furnitureCount.html());
	}
	
	function bindEvents() {
		$uiContainer.on('click', '[data-control="createRoom"]', function() {
			dialogs.CreateRoom.open({ date: getDate(), callback: loadData });
		});

		$uiContainer.on('click', '[data-control="createFurniture"]', function(event) {
			dialogs.CreateFurniture.open({
				date: getDate(),
				roomName: getRoomNameEl(event.target).html(),
				allFurniture: getAllFurniture(),
				callback: loadData
			});
		});

		$uiContainer.on('click', '[data-control="removeRoom"]', removeRoom);
		$uiContainer.on('click', '[data-control="addFurniture"]', addFurniture);
		$uiContainer.on('click', '[data-control="moveFurniture"]', moveFurniture);
	}

	function bindTemplates() {
		$.each(templates, function(index, template) {
			$.templates(template, '#' + template);
		});
	}

	function initialize(uiSelector) {
		$uiContainer = $(uiSelector);
		bindUi();
		initializeUi();
		bindEvents();
		bindTemplates();
	}

	tabs.Plan = {
		initialize: initialize,
		stopLoadData: stopLoadData,
		loadData: loadData
	};

})(App.Tabs, App.Urls, App.Dialogs);