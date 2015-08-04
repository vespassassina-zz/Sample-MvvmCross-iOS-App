# config.ru
require './app'

set :static, true
set :public_folder, 'public'

run App