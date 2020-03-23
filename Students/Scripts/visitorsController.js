visitorsApp.controller("visitorsController", ['$scope', '$http', '$window',
    function ($scope, $http) {

        $scope.data = null;
        $scope.errorMessage = "";
        $scope.showError = false;
        $scope.showTable = false;
        $scope.showAddNew = false;
        $scope.showRemoveButton = false;
        $scope.showSaveButton = true;
        $scope.showAddButton = true;
        $scope.cathedra = "";
        $scope.group = "";
        $scope.faculties = "";
        $scope.currentItem;
        $scope.persontypeEnum = [
            { id: 1, value: "Employee" },
            { id: 2, value: "Student" },
            { id: 3, value: "Entrant" },
            { id: 4, value: "Guest" }];

        $scope.GetData = function () {
            $scope.errorMessage = "";
            $scope.showError = false;
            var baseurl = location.protocol + "//" + window.location.host;
            var url = baseurl + "/api/persons/GetAll";
            var content = "";
            var requestConfig = {
                headers: {
                    'Content-Type': 'application/json'
                }
            };

            $http.post(url, content, requestConfig)
                .then(function (data) {
                    handleResponse(data);
                });
        };

        function handleResponse(message) {
            var messageInfo;
            var response = message.data;
            if (message.statusText !== null && message.statusText !== "OK") {
                messageInfo = message.statusText;
                ShowError(messageInfo);
            } else {
                if (response.length > 0) {
                    $scope.showTable = true;
                    $scope.data = response;
                } else {
                    messageInfo = "Visitors are not added";
                    ShowError(messageInfo);
                }
            }
        }

        function handleAddResponse(message) {
            var messageInfo;
            var response = message.data;
            if (message.statusText !== null && message.statusText !== "OK") {
                messageInfo = message.statusText;
                ShowError(messageInfo);
            } else {
                location.reload();
            }
        }

        function handleGetResponse(message) {
            var messageInfo;
            var response = message.data;
            if (message.statusText !== null && message.statusText !== "OK") {
                messageInfo = message.statusText;
                ShowError(messageInfo);
            } else {
                $scope.currentItem = response;
                setItem();
            }
        }

        function handleRemoveResponse(message) {
            var messageInfo;
            var response = message.data;
            if (message.statusText !== null && message.statusText !== "OK") {
                messageInfo = message.statusText;
                ShowError(messageInfo);
            } else {
                location.reload();
            }
        }

        function setItem() {
            $scope.firstname = $scope.currentItem.FirstName;
            $scope.lastname = $scope.currentItem.LastName;
            $scope.phonenumbers = $scope.currentItem.PhoneNumbers.join();
            $scope.persontype = $scope.persontypeEnum.find(a => a.id === $scope.currentItem.PersonType).value;
            $scope.faculties = $scope.currentItem.Faculties !== undefined && $scope.currentItem.Faculties !== null ? $scope.currentItem.Faculties.join() : "";
            $scope.group = $scope.currentItem.Group !== undefined && $scope.currentItem.Group !== null ? $scope.currentItem.Group : "";
            $scope.cathedra = $scope.currentItem.Cathedra !== undefined && $scope.currentItem.Cathedra !== null ? $scope.currentItem.Cathedra : "";
            $scope.reason = $scope.currentItem.Reason !== undefined && $scope.currentItem.Reason !== null ? $scope.currentItem.Reason : "";
            $scope.showAddNew = true;
            $scope.showAddButton = false;
            $scope.showSaveButton = false;
            $scope.showRemoveButton = true;
            $scope.showTable = false;
        }

        $scope.addNew = function () {
            $scope.showTable = false;
            $scope.showAddButton = false;
            $scope.showAddNew = true;
        }

        $scope.cancel = function () {
            location.reload();
        }

        $scope.openDetails = function (item) {

            var baseurl = location.protocol + "//" + window.location.host;
            var url = baseurl + "/api/persons/get";
            var body = { PersonType: item.PersonType, Id: item.Id };
            var content = JSON.stringify(body);
            var requestConfig = {
                headers: {
                    'Content-Type': 'application/json'
                }
            };

            $http.post(url, content, requestConfig)
                .then(function (data) {
                    handleGetResponse(data);
                });
        }

        $scope.submit = function () {

            $scope.errorMessage = "";
            $scope.showError = false;
            var baseurl = location.protocol + "//" + window.location.host;
            var url = baseurl + "/api/persons/add";
            var personTypeEnum = $scope.persontypeEnum.find(a => a.value === $scope.persontype).id;
            var numbers = $scope.phonenumbers.toString().split(',');
            var faculties = $scope.faculties.split(',');
            var body = { PersonType: personTypeEnum, FirstName: $scope.firstname, LastName: $scope.lastname, PhoneNumbers: numbers, Faculties: faculties, Group: $scope.group, Cathedra: $scope.cathedra, Reason: $scope.reason }
            var content = JSON.stringify(body);
            var requestConfig = {
                headers: {
                    'Content-Type': 'application/json'
                }
            };

            $http.post(url, content, requestConfig)
                .then(function (data) {
                    handleAddResponse(data);
                });
        }

        $scope.remove = function() {
            var baseurl = location.protocol + "//" + window.location.host;
            var url = baseurl + "/api/persons/remove";
            var body = { Id: $scope.currentItem.Id };
            var content = JSON.stringify(body);

            var requestConfig = {
                headers: {
                    'Content-Type': 'application/json'
                }
            };

            $http.post(url, content, requestConfig)
                .then(function (data) {
                    handleRemoveResponse(data);
                });
        }
    }])