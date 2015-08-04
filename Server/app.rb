# app.rb
require 'sinatra'

class App < Sinatra::Base

  get '/' do
  	indexPath = File.join(settings.root,'public','index.html')
  	if File.exists? (indexPath)  	
    	send_file indexPath
    else
    	""
    end
  end
  
  get '/test' do
    erb :test
  end
  
  post '/test' do
  
  	if params[:file].nil?
  		redirect 'test'
  		return
  	end
  	  
  	name = params[:file][:filename]
  	file = params[:file][:tempfile]  		
	
	dir = File.join(settings.root,'public')
	FileUtils.mkdir_p(dir) unless File.directory?(dir)

	path = File.join(dir,name)
  	File.open(path, 'wb') do |f|
    	f.write(file.read)
  	end  
  	
  	@filename = name  	
  	erb :show
  end
  
end