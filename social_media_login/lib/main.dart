import 'dart:html' as html;

import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';

final FirebaseAuth _auth = FirebaseAuth.instance;

void main() {
  String? token;
  if (html.window.location.href.contains("access_token")) {
    String url = html.window.location.href
        .replaceFirst("#/", "?"); // workaround for readable redirect url
    Uri uri = Uri.parse(url);
    if (uri.queryParameters.keys.contains("access_token")) {
      token = uri.queryParameters["access_token"];
    }
  }

  runApp(
    MaterialApp(
        title: 'Facebook Sign In',
        home: SignIn(
          token: token,
        )),
  );
}

class SignIn extends StatefulWidget {
  final String token;

  const SignIn({Key key, this.token}) : super(key: key);

  @override
  _SignInState createState() => _SignInState();
}

class _SignInState extends State<SignIn> {
  String? _message;
  final String clientId = "621324849743358";
  final String redirectUri = "http://localhost:49722/signin";

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    if (widget.token != null) _signInWithFacebook(widget.token);
  }

  void _signInWithFacebook(String token) async {
    setState(() {
      _message = "Loading...";
    });
    final AuthCredential credential = FacebookAuthProvider.credential(token);
    final User? user = (await _auth.signInWithCredential(credential)).user;
    assert(await user.getIdToken() != null);
    final User? currentUser = await _auth.currentUser();
    assert(user.uid == currentUser.uid);
    setState(() {
      if (user != null) {
        _message = 'Successfully signed in with Facebook. ' +
            user?.displayName.toString();
      } else {
        _message = 'Failed to sign in with Facebook. ';
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          children: <Widget>[
            Text(_message ?? "Please try to sign in"),
            ElevatedButton(
              onPressed: () {
                html.window.open(
                    "https://www.facebook.com/dialog/oauth?response_type=token&scope=email,public_profile,&client_id=${clientId}&redirect_uri=${redirectUri}",
                    "_self");
              },
              child: Text('Facebook login'),
            ),
          ],
        ),
      ),
    );
  }
}
