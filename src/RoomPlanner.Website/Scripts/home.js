(function(module) {

	'use strict';

	var tabs = {},
		tabIds = {
			Plan: '#tab-plan',
			History: '#tab-history'
		},
		$ui = {};

	function bindUi() {
		$ui = {
			tabsControl: $('[data-control="tabsControl"')
		};
	}

	function selectTab(newTabSelector) {
		$.each(tabs, function(index, tab) {
			tab.stopLoadData();
		});

		tabs[newTabSelector].loadData();
	}

	function tabActivated(event, info) {
		selectTab(info.newPanel.selector);
	}

	function initializeTabs() {
		tabs[tabIds.Plan] = module.Tabs.Plan;
		tabs[tabIds.History] = module.Tabs.History;

		$.each(tabs, function(index, tab) {
			tab.initialize(index);
		});
	}

	function initializeUi() {
		$ui.tabsControl.tabs({
			activate: tabActivated
		});
	}

	function initialize() {
		bindUi();
		initializeTabs();
		initializeUi();

		selectTab(tabIds.Plan);
	}

	module.Home = {
		initialize: initialize
	};

})(App);