using System;
using System.Threading.Tasks;
using StreamChat.Core;
using StreamChat.Core.Auth;
using StreamChat.Core.Events;
using StreamChat.Core.Exceptions;
using StreamChat.Core.Models;
using StreamChat.Core.Requests;
using StreamChat.Libs.Utils;
using UnityEngine;

// public class StreamChatBehaviour : MonoBehaviour
// {
//     public static StreamChatBehaviour instance;

// 	IStreamChatClient client;
// 	ChannelState channelState;

// 	public Message messagePrefab;
// 	Transform content;

// 	private void Awake()
// 	{
// 		instance = this;
// 		DontDestroyOnLoad(this);
// 	}

// 	public void SetContentObject()
// 	{
// 		content = GameObject.FindGameObjectWithTag("content").transform;
// 	}

// 	public void GetOrCreateClient(string userName)
// 	{
// 		string userId = StreamChatClient.SanitizeUserId(userName);
// 		AuthCredentials credentials = new AuthCredentials("qezen739wgfm", userId, StreamChatClient.CreateDeveloperAuthToken(userId));
// 		client = StreamChatClient.CreateDefaultClient(credentials);
// 		client.Connect();
// 		client.Connected += OnClientConnected;
// 		client.MessageReceived += OnMessageReceived;
// 	}

// 	void OnClientConnected()
// 	{
// 		Debug.Log("Stream client connected!");
// 	}

// 	private void Update()
// 	{
// 		if (client != null)
// 		{
// 			client.Update(Time.deltaTime);
// 		}
// 	}

// 	public void GetOrCreateChannel(string roomName)
// 	{
// 		GetOrCreateChannelAsync(roomName).LogIfFailed();
// 	}

// 	async Task GetOrCreateChannelAsync(string roomName)
// 	{
// 		ChannelGetOrCreateRequest request = new ChannelGetOrCreateRequest
// 		{
// 			State = true,
// 			Watch = true,
// 		};

// 		channelState = await client.ChannelApi.GetOrCreateChannelAsync("livestream", roomName, request);
// 	}

// 	public void SendChat(string message)
// 	{
// 		SendChatAsync(message, channelState.Channel.Type, channelState.Channel.Id).LogIfFailed();
// 	}

// 	async Task SendChatAsync(string message, string channelType, string channelId)
// 	{
// 		SendMessageRequest request = new SendMessageRequest
// 		{
// 			Message = new MessageRequest
// 			{
// 				Text = message
// 			}
// 		};

// 		await client.MessageApi.SendNewMessageAsync(channelType, channelId, request);
// 	}

// 	void OnMessageReceived(EventMessageNew newMessage)
// 	{
// 		Message newMessageSpawned = Instantiate(messagePrefab, content);
// 		newMessageSpawned.messageText.text = newMessage.Message.Text;
// 		newMessageSpawned.userNameText.text = newMessage.User.Id;
// 	}

// 	public void Disconnect()
// 	{
// 		client.MessageReceived -= OnMessageReceived;
// 		client.Dispose();
// 	}

// }
