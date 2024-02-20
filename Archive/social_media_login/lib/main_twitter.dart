import 'package:flutter/material.dart';
import 'package:twitter_oauth2_pkce/twitter_oauth2_pkce.dart';

void main() {
  runApp(const MaterialApp(home: Example()));
}

class Example extends StatefulWidget {
  const Example({Key? key}) : super(key: key);

  @override
  State<Example> createState() => _ExampleState();
}

class _ExampleState extends State<Example> {
  String? _accessToken;
  String? _refreshToken;

  @override
  Widget build(BuildContext context) => Scaffold(
        body: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text('Access Token: $_accessToken'),
              Text('Refresh Token: $_refreshToken'),
              ElevatedButton(
                onPressed: () async {
                  final oauth2 = TwitterOAuth2Client(
                    clientId: 'YOUR_CLIENT_ID',
                    clientSecret: 'YOUR_CLIENT_SECRET',
                    redirectUri: 'org.example.android.oauth://callback/',
                    customUriScheme: 'org.example.android.oauth',
                  );

                  final response = await oauth2.executeAuthCodeFlowWithPKCE(
                    scopes: Scope.values,
                  );

                  super.setState(() {
                    _accessToken = response.accessToken;
                    _refreshToken = response.refreshToken;
                  });
                },
                child: const Text('Push!'),
              )
            ],
          ),
        ),
      );
}
