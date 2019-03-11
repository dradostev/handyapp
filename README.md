# Handy.App
## Very simple demo project

Simple task manager combined with Telegram bot.

## Technology stack

* ASP.NET Core
* Vue.js
* PostgreSQL
* Docker

## Usage

You can add, remove and edit different types of drafts: *Notes* which are simple pieces of text, and *Reminders* which are basically alarms set on definite date and time to trigger.

This can be done both using web UI and Telegram bot.

### GUI screenshot
![GUI screenshot](https://i.imgur.com/w3IK1Vi.png)

### Telegram bot commands syntax

`/note [title] content` creates Note with given Title and Content.

`/remind [yyyy mm dd h:i] content` creates Reminder with given Content and sets it to given date and time to trigger.

## Build and run locally

1. Copy `.env.example` file to `.env` and set environment variables to desired.
2. Run `docker-compose build && docker-compose up`
3. Expose webhook URL and front-end URL via [ngrok](https://ngrok.com/) or [serveo](https://serveo.net/) (Telegram bot needs domain to work properly). Example: `ssh -R 80:127.0.0.1:80 serveo.net`