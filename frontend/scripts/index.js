const baseURL = "http://localhost:5117";
const defaultApp = "Complimenter";
const responseTextColor = "rgb(155, 155, 255)";
const responsePrefix = "⇥";
const requestTextColor = "rgb(155, 255, 155)";
const requestPrefix = "⇤";

const main = async () => {
    const consoleNames = await requestConsoleAppNamesHTTP();
    const consoleAppSelector = document.querySelector("#console-app-selector");
    consoleNames.forEach((appName) => {
        const optionElement = document.createElement("option");
        optionElement.value = appName;
        optionElement.text = appName;
        consoleAppSelector.appendChild(optionElement);
    });

    consoleAppSelector.addEventListener("change", async (event) => {
        const response = await switchAppHTTP(event.target.value);
        addMessageToHistory(response, false);
    });
    consoleAppSelector.value = defaultApp;

    var introduction = await switchAppHTTP(defaultApp);
    addMessageToHistory(introduction, false);

    window.addEventListener("keypress", ({ key }) => {
        if (key == "Enter") sendMessageFromTextInput();
    });
};

const sendMessageFromTextInput = async () => {
    const textElement = document.querySelector("input#text-input");
    addMessageToHistory(textElement.value);

    const responseMessage = await sendMessageHTTP(textElement.value);
    addMessageToHistory(responseMessage, false);

    textElement.value = "";
};

const addMessageToHistory = (message, isRequest = true) => {
    const lastMessageElement = document.querySelector("#message-history li");
    const messageHistoryElement = document.querySelector("#message-history");
    const listItemElement = document.createElement("li");
    listItemElement.style.color = isRequest ? requestTextColor : responseTextColor;
    listItemElement.textContent = `${isRequest ? requestPrefix : responsePrefix} ${message}`;
    messageHistoryElement.insertBefore(listItemElement, lastMessageElement);
};

const switchAppHTTP = async (appName) => {
    console.log(`Switching app to ${appName}`);
    const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ Content: appName }),
    };
    const response = await fetch(`${baseURL}/switch-app`, requestOptions);
    const content = await response.json();
    return content.content;
};

const sendMessageHTTP = async (message) => {
    console.log("sendMessageHTTP: " + message);
    const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ Content: message }),
    };
    const response = await fetch(`${baseURL}/message`, requestOptions);
    const responseContent = await response.json();
    return responseContent.content;
};

const requestConsoleAppNamesHTTP = async () => {
    const requestOptions = {
        method: "GET",
        headers: { "Content-Type": "application/json" },
    };
    const response = await fetch(`${baseURL}/console-app-names`, requestOptions);
    const content = await response.json();
    console.log(content);
    return content;
};

main();
